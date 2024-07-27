using UnityEngine;

// This script is a ScriptableObject that will be used to store the weapon details
[CreateAssetMenu(fileName = "WeaponsData", menuName = "ScriptableObject/WeaponDetails", order = 2)]

public class WeaponsData : ScriptableObject
{
    public float Damage;
    public float FireAccuracy;
    public float TurnSpeed;
    public float ReloadTime;
    public float AimAccuracy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
