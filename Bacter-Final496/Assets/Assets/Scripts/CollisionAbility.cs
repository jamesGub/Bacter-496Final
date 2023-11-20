using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAbility : MonoBehaviour
{
    private bool abilityUnlocked = false;

    public void UnlockAbility()
    {
        abilityUnlocked = true;
        Debug.Log("A: Collision ability unlocked!");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.tag);
        if (abilityUnlocked && other.tag == "AIPlayer")
        {
            Debug.Log("Collided with: " + other.tag);
            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                Debug.Log("Collision is successful");
                enemyController.Damage(enemyController.DamageOther());
            }
            else
            {
                Debug.LogError("EnemyController component not found on the collided object.");
            }

            HealthSystem healthSystem = GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                Debug.Log("Damage taken");
                healthSystem.DamagePlayer(healthSystem.collisionDamage);
            }
            else
            {
                Debug.LogError("HealthSystem component not found on this gameObject.");
            }
        }

    }
}
