using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AbilityPress : MonoBehaviour
{
    public MitosisGauge mitosisGauge;
    public HealthSystem maxHealth;
    public HealthSystem currentHealth;
    public PlayerController moveSpeed; 
    public PlayerController playerController;
    public ShieldAbility shieldAbility;

    public void ApplyLysosomicAbility() {
            if (mitosisGauge != null) { 
                mitosisGauge.ApplyLysosomicAbility();
                Debug.Log("Health is now: " + maxHealth);
                Debug.Log("Movement speed is now: " + moveSpeed);
                Debug.Log("Current Health: " + currentHealth);
            }
        }

    public void ApplyNimbleAbility() { 
        if (mitosisGauge != null) { 
            mitosisGauge.UnlockNimble();
            mitosisGauge.ApplyNimble(); 
            Debug.Log("Movement ability unlocked"); 
        }
    }

    public void UnlockShieldAbility() {
        if (mitosisGauge != null) {
            mitosisGauge.UnlockShield();
        }
    }

    public void UnlockCollisionAbility() {
        Debug.Log("Ability Collision Check");
        if (mitosisGauge != null) {
            mitosisGauge.UnlockCollision();
        }
    }
        
}
