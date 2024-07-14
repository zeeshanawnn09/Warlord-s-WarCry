using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehavior : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform endPoint;
    public int maxSpawn = 10;


    [SerializeField] private float _spawnRate = 0.3f;
    [SerializeField] private int _spawnTime = 1;


    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawner", _spawnTime, _spawnRate); 
    }

    void Spawner()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<Endpoint>().destination = endPoint;
        count++;
       
    }

    private void Update()
    {
        if ( count == maxSpawn)
        {
            CancelInvoke("Spawner");
        }
        else
        {
            Spawner();
        }
    }
}
