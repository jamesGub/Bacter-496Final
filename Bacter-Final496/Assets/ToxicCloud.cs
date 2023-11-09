using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicCloud : MonoBehaviour
{
    public ScreenShake screenShake;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.healthDecreaseRate = 15f;
                if(screenShake != null) { 
                    screenShake.Rumble(); 
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.healthDecreaseRate = 5f;
            }
        }
    }
}
