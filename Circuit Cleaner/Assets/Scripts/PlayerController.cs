using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public GameObject playerCamera;
    public GameObject topCamera;
    private Animator animator;
    private AudioSource runSound;
    private AudioSource ambientSound;

    public Image hpBar;
    private float hpBarX;
    private float hpBarWidth = 300;
    private float initialWidth;

    public GameObject gameMenuPanel;
    private GameObject laser;

    private float hp = 100;
    public float damage;

    private bool canFire = true;
    private GameObject cameraPoint;
    private bool stepSound;

    //private int raycastRange = 5000;

    private bool pause = false;
    private bool moving = false;
    private bool stunned = false;

    void Start () {
        animator = GetComponent<Animator>();
        runSound = GetComponent<AudioSource>();
        cameraPoint = transform.GetChild(0).gameObject;
        laser = playerCamera.transform.GetChild(1).gameObject;

        hpBarX = 155;
        initialWidth = 300;

        ambientSound = playerCamera.GetComponent<AudioSource>();

        BasicEnemyBehaviour.pause = false;
        pause = false;

        if (StaticVariables.music)
        {
            ambientSound.enabled = true;
        }
        else
        {
            ambientSound.enabled = false;
        }

        if (!StaticVariables.sfx)
        {
            laser.GetComponent<AudioSource>().enabled = false;
        }

        Invoke("regeneration", 1);
    }

    void FixedUpdate () {

        if (!pause)
        {
            Cursor.visible = false;
        }
        
        moving = false;
        stepSound = StaticVariables.sfx;

        topCamera.transform.position = new Vector3(transform.position.x, 40, transform.position.z);
        if (!pause && !stunned)
        {
            animator.SetInteger("VerticalMovement", 0);
            animator.SetInteger("HorizontalMovement", 0);

            transform.Rotate(0, Input.GetAxis("Mouse X") * 3 * StaticVariables.mouseSensibility, 0);

            playerCamera.transform.Rotate(-(Input.GetAxis("Mouse Y") * StaticVariables.mouseSensibility), Input.GetAxis("Mouse X") * 3 * StaticVariables.mouseSensibility, 0);
            playerCamera.transform.eulerAngles = new Vector3(playerCamera.transform.eulerAngles.x, playerCamera.transform.eulerAngles.y, 0);

            playerCamera.transform.position = new Vector3(cameraPoint.transform.position.x, cameraPoint.transform.position.y, cameraPoint.transform.position.z);

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, playerCamera.transform.eulerAngles.y, transform.eulerAngles.z);

            if (Input.GetKey(KeyCode.W))
            {
                if (stepSound)
                {
                    runSound.enabled = true;
                    moving = true;
                }
                transform.Translate(Vector3.forward / 7);
                animator.SetInteger("VerticalMovement", 1);
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (stepSound)
                {
                    runSound.enabled = true;
                    moving = true;
                }
                transform.Translate(Vector3.back / 10);
                animator.SetInteger("VerticalMovement", -1);
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (stepSound)
                {
                    runSound.enabled = true;
                    moving = true;
                }
                transform.Translate(Vector3.left / 10);
                animator.SetInteger("HorizontalMovement", -1);
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (stepSound)
                {
                    runSound.enabled = true;
                    moving = true;
                }
                transform.Translate(Vector3.right / 10);
                animator.SetInteger("HorizontalMovement", 1);
            }

            if(!moving) 
            {
                runSound.enabled = false;
            }

            Vector3 direction = playerCamera.transform.TransformDirection(Vector3.forward);
            Vector3 origin = playerCamera.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(origin, direction, out hit) && Input.GetMouseButton(0) && canFire)
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<BasicEnemyBehaviour>().takeDamage((int)damage, this);
                }
                laser.SetActive(true);
                canFire = false;
                StartCoroutine(wait(0.5f));
            }
            //Debug.DrawLine(origin, direction * raycastRange);
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            BasicEnemyBehaviour.pause = true;
            gameMenuPanel.SetActive(true);
            pause = true;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            BasicEnemyBehaviour.pause = false;
            gameMenuPanel.SetActive(false);
            pause = false;
            Cursor.visible = false;
        }
    }

    private void changedHP(float newHP)
    {
        hpBarWidth = newHP / 100;
        hpBarX = 15 + (newHP / 2) * (initialWidth / 100);

        hpBar.transform.localScale = new Vector3(hpBarWidth, 1, 1);
        hpBar.rectTransform.anchoredPosition = new Vector3(hpBarX, 0, 0);
        print(newHP);
    }

    private void win()
    {
        pause = true;
        gameMenuPanel.SetActive(true);
        gameMenuPanel.transform.GetChild(0).GetComponent<Text>().text = "You Won !";
        Cursor.visible = true;
        animator.SetInteger("HorizontalMovement", 0);
        animator.SetInteger("VerticalMovement", 0);
        runSound.enabled = false;
    }

    public void takeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
        changedHP(hp);

        if (hp <= 0)
        {
            dead();
        }
    }

    public void stun(int seconds)
    {
        stunned = true;
        Invoke("endStun", seconds);
    }

    private void endStun()
    {
        stunned = false;
    }

    private void dead()
    {
        pause = true;
        gameMenuPanel.SetActive(true);
        gameMenuPanel.transform.GetChild(0).GetComponent<Text>().text = "Game Over !";
        Cursor.visible = true;
        Destroy(this.gameObject);
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
        laser.SetActive(false);
    }

    public void destroyedEnemy()
    {
        damage += 0.5f;
    }

    private void regeneration()
    {
        if (hp < 100 && !pause)
        {
            hp += 1;
            changedHP(hp);
        }

        if (EnemyCounter.remainedEnemies == 0)
        {
            win();
        }

        Invoke("regeneration", 1);
    }
}
