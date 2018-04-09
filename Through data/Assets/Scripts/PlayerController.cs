using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject playerCamera;
    private Animator animator;

    private int hp = 100;

    private bool canFire = true;
    private GameObject cameraPoint;

    private int raycastRange = 5000;

    void Start () {
        animator = GetComponent<Animator>();
        cameraPoint = transform.GetChild(0).gameObject;
	}
	
	void FixedUpdate () {

        animator.SetInteger("VerticalMovement", 0);
        animator.SetInteger("HorizontalMovement", 0);

        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);

        playerCamera.transform.Rotate(-(Input.GetAxis("Mouse Y") / 2), Input.GetAxis("Mouse X"), 0);
        playerCamera.transform.eulerAngles = new Vector3(playerCamera.transform.eulerAngles.x, playerCamera.transform.eulerAngles.y, 0);

        playerCamera.transform.position = new Vector3(cameraPoint.transform.position.x, cameraPoint.transform.position.y, cameraPoint.transform.position.z);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, playerCamera.transform.eulerAngles.y, transform.eulerAngles.z);

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward / 7);
            animator.SetInteger("VerticalMovement", 1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back / 10);
            animator.SetInteger("VerticalMovement", -1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left / 10);
            animator.SetInteger("HorizontalMovement", -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right / 10);
            animator.SetInteger("HorizontalMovement", 1);
        }

        Vector3 direction = playerCamera.transform.TransformDirection(Vector3.forward);
        Vector3 origin = playerCamera.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit) && Input.GetMouseButton(0))
        {
            if (hit.collider.gameObject.tag == "Enemy" && canFire)
            {
                hit.collider.gameObject.GetComponent<BasicEnemyBehaviour>().takeDamage(10);
                canFire = false;
                StartCoroutine(wait(0.5f));
            }
        }
        Debug.DrawLine(origin, direction * raycastRange);
    }

    public void takeDamage(int damage)
    {
        hp -= damage;
        print(hp);

        if (hp <= 0)
        {
            dead();
        }
    }

    private void dead()
    {
        Destroy(this.gameObject);
    }


    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }
}
