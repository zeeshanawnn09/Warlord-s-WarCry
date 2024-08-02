using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPoolFunction : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public AudioSource explodeSound;

    //this is the pool that the particle system will return to
    public IObjectPool<ParticleSystem> pool;

    // Start is called before the first frame update
    void Start()
    {
        //this is the function that will be called when the particle system stops
        var main = particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;

        pool = LevelManager.explosionpool;

    }

    void OnParticleSystemEnabled()
    {
        explodeSound.Play();
    }

    void OnParticleSystemStopped()
    {
        //return the particle system to the pool
        pool.Release(particleSystem);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
