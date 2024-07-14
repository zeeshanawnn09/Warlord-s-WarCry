using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject[] spawnPoints;
    static int totalEnemies = 0;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        foreach (GameObject sp in spawnPoints)
        {
            totalEnemies += sp.GetComponent<SpawnBehavior>().maxSpawn;
        }
    }

    public static void EnemyDied()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
        {
            Debug.Log("Level Over");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
