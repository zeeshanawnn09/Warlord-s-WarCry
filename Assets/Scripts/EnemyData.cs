using UnityEngine;

// This script is a ScriptableObject that will be used to store the enemy details
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyDetails", order = 1)]
public class EnemyData : ScriptableObject
{
    public string Name;
    public float speed;
    public int maxHealth;
    public int Reward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
