using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : MonoBehaviour
{
    public GameObject shieldPrefab; 
    private GameObject shieldInstance;
    private bool shieldActive = false;
    public MitosisGauge mitosisGauge;

    void Update()
    {
        if (shieldActive) {
            MoveShield();
        }

        if (Input.GetKeyDown(KeyCode.E) && mitosisGauge != null && mitosisGauge.IsShieldUnlocked()) {
            ToggleShield();
        }
    }

    void ToggleShield()
    {
     if (mitosisGauge.UseMitosisGauge(50)) {
            if (shieldActive) {
                Destroy(shieldInstance);
                shieldActive = false;
            } else {
                SpawnShield();
                shieldActive = true;
            }
        }
        else {
            Debug.Log("Not enough Mitosis gauge to activate.");
        }
    }
    


    void MoveShield() {
        if (shieldInstance != null) {
            shieldInstance.transform.position = transform.position;
        }
    }

    void SpawnShield() {
       if (shieldPrefab != null){
            shieldInstance = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (shieldActive && other.CompareTag("ToxicCloud")) {
            Destroy(other.gameObject);
        }
    }
}
