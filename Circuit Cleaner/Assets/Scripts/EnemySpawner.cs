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
    System.Random random;
    public GameObject map;
    private int numberOfEnemyes = 5;
    private GameObject player;

    public GameObject[] proceduralPieces;
    // 0 - 3 ways
    // 1 - corner
    // 2 - intersection
    // 3 - passage

    int z = -70;
    int x = -70;

    void Awake () {
        player = GameObject.Find("Player");
        random = new System.Random();

        if (StaticVariables.gameMode == "normal")
        {
            map.SetActive(true);
            spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        }
        else if (StaticVariables.gameMode == "survival")
        {
            generateMap(0);
            spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
            createNewWave();
        }

        int id = 0;
        for (int i = 0; i < numberOfEnemyes; i++)
        {
            for (int j = 0; j < enemies.Length; j++)
            {
                int index = random.Next(spawnPoints.Length);
                while (Vector3.Distance(player.transform.position, spawnPoints[index].transform.position) < 50)
                {
                    index = random.Next(spawnPoints.Length);
                }
                GameObject clone = Instantiate(enemies[j], spawnPoints[index].transform.position, Quaternion.identity);
                clone.name = enemies[j].name + id;
                id++;
            }
        }        
    }


    private void createNewWave()
    {
        for (int i = 0; i < numberOfEnemyes; i++)
        {
            for (int j = 0; j < enemies.Length; j++)
            {
                int index = random.Next(spawnPoints.Length);
                GameObject clone = Instantiate(enemies[j], spawnPoints[index].transform.position, Quaternion.identity);
                clone.name = enemies[j].name;
            }
        }
        numberOfEnemyes++;
        Invoke("createNewWave", 20);
    }

    private void generateMap(int index)
    {
        /*
        Instantiate(proceduralPieces[index], new Vector3(x, 0, z), Quaternion.identity);

        if (index == 1)
        {
            x += 10;
            index = 2;
            if (-180 < x && x < 75 && -70 < z && z < 125)
            {
                generateMap(index);
            }
        }
        else if (index == 2)
        {
            int lastX = x;
            z += 10;

            index = random.Next(proceduralPieces.Length);
            if (-180 < x && x < 75 && -70 < z && z < 125)
            {
                generateMap(index);
            }

            x = lastX;

            x += 10;
            print(x);
            index = 2;
            if (-180 < x && x < 75 && -70 < z && z < 125)
            {
                generateMap(index);
            }
        }
        else if (index == 0 || index == 3)
        {
            index = random.Next(proceduralPieces.Length);
            z += 10;

            if (-180 < x && x < 75 && -70 < z && z < 125)
            {
                generateMap(index);
            }
        }
        */

        for (int i = -180; i < 75; i += 10)
        {
            for (int j = -70; j < 125; j += 10)
            {
                index = random.Next(proceduralPieces.Length);
                Instantiate(proceduralPieces[index], new Vector3(i, 0, j), Quaternion.identity);
                
            }
        }
        
    }
}
