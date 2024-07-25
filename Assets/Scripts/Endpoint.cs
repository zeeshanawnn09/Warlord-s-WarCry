using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Endpoint : MonoBehaviour
{
    public Transform destination;
    public NavMeshAgent agent;
    public EnemyData enemyData;
    public Slider HealthBar_Prefab;
    Slider HealthBar;

    int curr_health;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(destination.position);

        agent.speed = enemyData.speed;

        curr_health = enemyData.maxHealth;
        HealthBar = Instantiate(HealthBar_Prefab, transform.position, Quaternion.identity);
        HealthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        
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

        if (HealthBar)
        {
            HealthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up * 1.2f);
        }
    }
   
}
