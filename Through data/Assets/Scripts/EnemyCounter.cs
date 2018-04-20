using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {

    public static int remainedEnemies;

	void Start () {
        Invoke("countEnemies", 1);
	}

    void countEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesList = new List<GameObject>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].transform.position.y < -10)
            {
                Destroy(enemies[i]);
            }
            else
            {
                enemiesList.Add(enemies[i]);
            }
        }

        remainedEnemies = enemiesList.Count;
        print(remainedEnemies);
        Invoke("countEnemies", 1);
    }

}
