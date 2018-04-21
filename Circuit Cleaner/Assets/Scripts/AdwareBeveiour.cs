using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicEnemyBehaviour))]
public class AdwareBeveiour : BasicEnemyBehaviour
{

    public int range = 50;
    public int divider2 = 5;
    public int distanceFromPlayer = 10;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        setDivider(divider2);
        setDamage(1);
    }

    void Update()
    {
        goToPlayer(player, range, distanceFromPlayer);
    }
}
