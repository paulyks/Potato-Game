using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemies;
    // 1 - Adware
    // 2 - Spyware
    // 3 - Worm
    // 4 - Trojan 

    private GameObject[] spawnPoints;

    void Start () {

        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        System.Random random = new System.Random();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < enemies.Length; j++)
            {
                int index = random.Next(spawnPoints.Length);
                GameObject clone = Instantiate(enemies[j], spawnPoints[index].transform.position, Quaternion.identity);
                clone.name = enemies[j].name;
            }
        }
	}
}
