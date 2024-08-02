using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class LevelManager : MonoBehaviour
{
    GameObject[] spawnPoints;
    static int totalEnemies = 0;

    public ParticleSystem explosionParticle;
    public static IObjectPool<ParticleSystem> explosionpool;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        foreach (GameObject sp in spawnPoints)
        {
            totalEnemies += sp.GetComponent<SpawnBehavior>().maxSpawn;
        }

        //create the object pool
        explosionpool = new ObjectPool<ParticleSystem>(CreateDeathExplosion, OnTakenFromPool, OnReturnToPool, null, true, 10, 20);
    }

    ParticleSystem CreateDeathExplosion()
    {
        ParticleSystem ps = Instantiate(explosionParticle);
        ps.Stop();
        return ps;
    }

    void OnReturnToPool(ParticleSystem system)
    {
        system.gameObject.SetActive(false);
    }

    void OnTakenFromPool(ParticleSystem system)
    {
        system.gameObject.SetActive(true);
    }

    public static void CreateExplosion(Vector3 position)
    {
        ParticleSystem DeathExplosion = explosionpool.Get();
        if(DeathExplosion != null)
        {
            DeathExplosion.transform.position = position;
            DeathExplosion.Play();
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
