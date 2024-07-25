using UnityEngine;

// This script is a ScriptableObject that will be used to store the enemy details
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyDetails", order = 5)]
public class EnemyData : ScriptableObject
{
    public string Name;
    public float speed;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
