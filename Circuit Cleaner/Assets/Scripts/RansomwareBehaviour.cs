using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicEnemyBehaviour))]
public class RansomwareBehaviour : BasicEnemyBehaviour {

    public int range = 50;
    public int divider2 = 10;
    public int distanceFromPlayer = 10;

    private GameObject player;
    private Animator animator;

    private bool canStun = true;

    void Start () {
        player = GameObject.Find("Player");
        setDivider(divider2);
        setDamage(50);
        setHpBarHeight(0.35f);

        animator = GetComponent<Animator>();
        Invoke("refreshSnare", 5);
    }

    // Update is called once per frame
    void Update ()
    {
        goToPlayer(player, range, distanceFromPlayer);

        if (player)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < range)
            {
                if (Vector3.Distance(player.transform.position, transform.position) > distanceFromPlayer)
                {
                    animator.SetBool("moveForward", true);
                }
                else
                {
                    animator.SetBool("moveForward", false);

                    if (canStun)
                    {
                        player.GetComponent<PlayerController>().stun(1);
                        canStun = false;
                        animator.SetBool("attack", true);
                    }
                    else
                    {
                        animator.SetBool("attack", false);
                    }
                }
            }
        }
    }

    private void refreshSnare()
    {
        canStun = true;
        Invoke("refreshSnare", 5);
    }
}
