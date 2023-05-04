using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
   [SerializeField] int maxHitPoints = 5;
   [SerializeField] int diffucltyAdd = 1;
   [SerializeField] int currentHitPoints;

   Enemy enemy;

     void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void Start() {
        enemy = GetComponent<Enemy>();
    }
    void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    void ProcessHit() {
        currentHitPoints --;
        if (currentHitPoints <= 0) {
          gameObject.SetActive(false);
          maxHitPoints+=diffucltyAdd;
          enemy.Reawrd();
        }
    }
}
