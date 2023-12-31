using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAbility : MonoBehaviour
{
    private bool abilityUnlocked = false;
    MitosisGauge mitosisGauge;

    public void UnlockAbility()
    {
        Debug.Log("Collision ability unlocked!");
        mitosisGauge.UnlockCollision();  
        abilityUnlocked = true;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (abilityUnlocked)
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.Damage(enemyController.DamageOther());
            }
            else
            {
                Debug.LogError("EnemyController component not found on the collided object.");
            }

            HealthSystem healthSystem = GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.DamagePlayer(healthSystem.collisionDamage);
            }
            else
            {
                Debug.LogError("HealthSystem component not found on this gameObject.");
            }
        }
    }
}
