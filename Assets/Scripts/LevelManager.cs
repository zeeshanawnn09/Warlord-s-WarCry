using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Pool;

public class LevelManager : MonoBehaviour
{
    SpawnBehavior[] spawnPoints;
    static int totalEnemies = 0;

    public ParticleSystem explosionParticle;
    public static IObjectPool<ParticleSystem> explosionpool;

    static int TotalWaves = 3;
    static int WavesEmitted = 0;
    static bool LvlOver = false;
    static bool nxtWave = false;

    int TimeBetweenWaves = 50;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 30;
        GameObject[] spawnP = GameObject.FindGameObjectsWithTag("Spawn");
        spawnPoints = new SpawnBehavior[spawnP.Length]; 
        for (int i = 0; i < spawnP.Length; i++)
        {
            spawnPoints[i] = spawnP[i].GetComponent<SpawnBehavior>();
            totalEnemies += spawnPoints[i].maxSpawn;
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
            WavesEmitted++;
            nxtWave = true;
            if (WavesEmitted == TotalWaves)
            {
                Debug.Log("Level Over");
                LvlOver = true;
                nxtWave = false;
            }
        }
    }

    void ResetSpawner()
    {
        foreach (SpawnBehavior sp in spawnPoints)
        {
            totalEnemies += sp.maxSpawn;
            sp.Restart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nxtWave)
        {
            nxtWave = false;
            Invoke("ResetSpawner", TimeBetweenWaves);
        }
    }
}
