using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
      [SerializeField] Transform target;
      [SerializeField] ParticleSystem projector;
      [SerializeField] Transform weapon;
      [SerializeField] int range = 10;

   void Start()
{
    Vector3 originalRotation = weapon.transform.rotation.eulerAngles;
    weapon.transform.rotation = Quaternion.Euler(90, originalRotation.y, originalRotation.z);
    target = FindObjectOfType<EnemyMover>().transform;
}

    void Update()
    {
    FindClosestTarget();
    AimWeapon();
    }

    void FindClosestTarget() {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closest = null;
        float MaxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies) {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < MaxDistance) {
                closest = enemy.transform;
                MaxDistance = targetDistance;
            }
        }
        target = closest;
    }
    void AimWeapon() {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);
        if (targetDistance < range ){
        Attack(true); 
        } else {
        Attack(false);
        }
    }

    void Attack(bool isActive) 
    {
    var emissionMoudle = projector.emission;
    emissionMoudle.enabled = isActive;
    }
}
