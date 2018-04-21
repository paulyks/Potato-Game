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

    public static bool pause = false;

    private float hpBarHeight = 0.15f;

    void Start()
    {
        hpBar = transform.GetChild(0).gameObject;
        initialSizeOfHPBar = (int)hpBar.transform.localScale.x;
    }

    protected void setHpBarHeight(float height)
    {
        hpBarHeight = height;
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
        if (!pause)
        {
            if (obj != null)
            {
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
            else
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    public void takeDamage(int damage, PlayerController player)
    {
        hp -= damage;
        hpBar.transform.localScale = new Vector3((initialSizeOfHPBar * (hp / 100)), hpBarHeight, 0.001f);
        if (hp <= 0)
        {
            player.destroyedEnemy();
            dead();
        }
    }

    private void dead()
    {
        Destroy(this.gameObject);
    }
}
