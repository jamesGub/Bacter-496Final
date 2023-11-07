using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PelletPickup : MonoBehaviour
{
    public AudioSource BubblePop;   
    public AudioClip popN;
    public float minPitch;
    public float maxPitch;
    public float healthAmount = 25.0f; 
    public float gaugeAmount = 5.0f;

   
void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FoodPellet"))
        {
            if (BubblePop != null && popN != null) { 
                float randomPitch =  Random.Range(minPitch, maxPitch);
                BubblePop.pitch = randomPitch;
                BubblePop.PlayOneShot(popN);
            }

            HealthSystem healthSystem = GetComponent<HealthSystem>(); 
            if (healthSystem != null)
            {
                healthSystem.CollectFoodPellet(healthAmount); 
            }
            
            MitosisGauge mitosisGauge = GetComponent<MitosisGauge>();
            if (mitosisGauge != null) { 
                mitosisGauge.CollectFoodPellet(gaugeAmount);
            }

            Destroy(other.gameObject); 
    }
}
}

