using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicEnemyBehaviour))]
public class WormBehaveiour : BasicEnemyBehaviour {

    public int range = 50;
    public int divider2 = 2;
    public int distanceFromPlayer = 10;

    private GameObject player;

    void Start () {
        player = GameObject.Find("Player");
        setDivider(divider2);
        Invoke("clone", 60);
        setDamage(5);
        setHpBarHeight(0.35f);
    }

    void Update () {
        goToPlayer(player, range, distanceFromPlayer);
    }

    private void clone()
    {
        Instantiate(this, transform.position, transform.rotation);
        Invoke("clone", 60);
    }
}
