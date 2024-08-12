using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehavior : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform endPoint;
    
    public int maxSpawn = 50;
    public float spawnRate = 5;
    public float spawnDelay = 3.0f;

    int count = 0;

    // Start is called before the first frame update
    private void Start()
    {
        Restart();
    }
    public void Restart()
    {
        count = 0;
        InvokeRepeating("Spawner", spawnDelay, spawnRate); 
    }

    void Spawner()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<Endpoint>().destination = endPoint;
        count++;
        if (count >= maxSpawn)
        {
            CancelInvoke("Spawner");
        }
    }
}
