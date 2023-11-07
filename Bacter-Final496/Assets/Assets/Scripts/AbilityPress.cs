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

    public void ApplyLysosomicAbility() {
            if (mitosisGauge != null) { 
                mitosisGauge.ApplyLysosomicAbility();
                Debug.Log("Health is now: " + maxHealth);
                Debug.Log("Movement speed is now: " + moveSpeed);
                Debug.Log("Current Health: " + currentHealth);
            }
        }
}
