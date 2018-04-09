using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehaviour : MonoBehaviour {

    private int divider = 2;
    private float hp = 100;
    private float initialSizeOfHPBar;

    private bool canFire = true;

    private GameObject hpBar;
    public static bool foundPlayer = false;
    private int damage = 0;

    void Start()
    {
        hpBar = transform.GetChild(0).gameObject;
        initialSizeOfHPBar = (int)hpBar.transform.localScale.x;
    }

    protected void setDamage(int damage)
    {
        this.damage = damage;
    }

    protected void setDivider(int divider)
    {
        this.divider = divider;
    }

    protected void goToPlayer(GameObject obj, int range, int distanceFromPlayer)
    {
        if (obj != null)
        if (Vector3.Distance(obj.transform.position, transform.position) < range || foundPlayer)
        {
            transform.LookAt(obj.transform.position);

            if (Vector3.Distance(obj.transform.position, transform.position) > distanceFromPlayer)
            {
                transform.Translate(Vector3.forward / divider);
            }
            else
            {
                if (canFire)
                {
                    canFire = false;
                    obj.GetComponent<PlayerController>().takeDamage(damage);
                    StartCoroutine(wait(1));
                }
            }
        }
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    public void takeDamage(int damage)
    {
        hp -= damage;
        hpBar.transform.localScale = new Vector3((initialSizeOfHPBar * (hp / 100)), 0.35f, 0.35f);
        print(gameObject.name + " " + hp);
        if (hp <= 0)
        {
            dead();
        }
    }

    private void dead()
    {
        Destroy(this.gameObject);
    }
}
