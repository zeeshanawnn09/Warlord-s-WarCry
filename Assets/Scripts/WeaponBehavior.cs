using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    GameObject curr_target;

    public GameObject turret_core;
    public GameObject turret_barrel;

    Quaternion turret_core_start_rotation;
    Quaternion turret_barrel_start_rotation;

    // Start is called before the first frame update
    void Start()
    {
        turret_core_start_rotation = turret_core.transform.rotation;
        turret_barrel_start_rotation = turret_barrel.transform.rotation;
    }

    //when target enters the collider of weapon
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("goob") && curr_target == null)
        {
            curr_target = other.gameObject;
        }
    }

    //when target exits the collider of weapon
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == curr_target)
        {
            curr_target = null;
        }
    }

    public void TurretMovement()
    {
        if (curr_target != null)
        {
            Vector3 aimAt = new Vector3(curr_target.transform.position.x, turret_core.transform.position.y, curr_target.transform.position.z);
            
            float Dist2Target = Vector3.Distance(aimAt, turret_barrel.transform.position);

            Vector3 RelTargetPos = turret_barrel.transform.position + (turret_barrel.transform.forward * Dist2Target);

            RelTargetPos = new Vector3(RelTargetPos.x, curr_target.transform.position.y, RelTargetPos.z);

            turret_barrel.transform.rotation = Quaternion.Slerp(turret_barrel.transform.rotation,
                Quaternion.LookRotation(RelTargetPos - turret_barrel.transform.position), Time.deltaTime);

            turret_core.transform.rotation = Quaternion.Slerp(turret_core.transform.rotation, Quaternion.LookRotation(aimAt - turret_core.transform.position), Time.deltaTime);
        }

        else
        {
            turret_barrel.transform.rotation = Quaternion.Slerp(turret_barrel.transform.rotation,
               turret_barrel_start_rotation, Time.deltaTime);

            turret_core.transform.rotation = Quaternion.Slerp(turret_core.transform.rotation, turret_core_start_rotation, Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TurretMovement();
    }
}
