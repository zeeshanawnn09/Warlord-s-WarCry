using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Endpoint : MonoBehaviour
{
    public Transform destination;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        //when player reaches a certain point in the game, it'll be destroyed
        if (agent.remainingDistance < 0.5f && agent.hasPath)
        {
            LevelManager.EnemyDied();
            agent.ResetPath();
            Destroy(this.gameObject, 0.1f);
        }
    }
   
}
