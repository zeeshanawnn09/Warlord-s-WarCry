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

    Endpoint curr_target_code;



    // Start is called before the first frame update
    void Start()
    {
        turret_core_start_rotation = turret_core.transform.rotation;
        turret_barrel_start_rotation = turret_barrel.transform.localRotation;
    }

    //when target enters the collider of weapon
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("goob") && curr_target == null)
        {
            curr_target = other.gameObject;
            curr_target_code = curr_target.GetComponent<Endpoint>();
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

    //when the bullet hits the target
    void BulletBehavior()
    {
        if (curr_target)
        {
            curr_target_code.OnPlayerHit(1);
        }
    }

    public void TurretMovement()
    {
        if (curr_target != null)
        {
            //aiming at the target
            Vector3 aimAt = new Vector3(curr_target.transform.position.x, turret_core.transform.position.y, curr_target.transform.position.z);

            //distance between the turret and the target
            float Dist2Target = Vector3.Distance(aimAt, turret_barrel.transform.position);

            //relative position of the target
            Vector3 RelTargetPos = turret_barrel.transform.position + (turret_barrel.transform.forward * Dist2Target);

            RelTargetPos = new Vector3(RelTargetPos.x, curr_target.transform.position.y, RelTargetPos.z);
            
            //rotating the turret
            turret_barrel.transform.rotation = Quaternion.Slerp(turret_barrel.transform.rotation,
                Quaternion.LookRotation(RelTargetPos - turret_barrel.transform.position), Time.deltaTime);

            //rotating the core of the turret
            turret_core.transform.rotation = Quaternion.Slerp(turret_core.transform.rotation, Quaternion.LookRotation(aimAt - turret_core.transform.position), Time.deltaTime);

            //making sure the turret is facing the target
            Vector3 GunFaceTowardsTarget = curr_target.transform.position - turret_barrel.transform.position;

            //if the angle between the turret and the target is less than 10 degrees, shoot the bullet
            if (Vector3.Angle(GunFaceTowardsTarget, turret_barrel.transform.forward) < 10)
                {
                    BulletBehavior();
                }
        }

        else
        {
            //if there is no target, reset the turret to its original position
            turret_barrel.transform.localRotation = Quaternion.Slerp(turret_barrel.transform.localRotation,
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
