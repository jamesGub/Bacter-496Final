using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldAbility : MonoBehaviour
{
    public GameObject shieldPrefab; 
    private GameObject shieldInstance;
    private bool shieldActive = false;
    public TMP_Text coilText;
    public MitosisGauge mitosisGauge;


    void Update()
    {
        if (shieldActive) {
            MoveShield();
            
        } 

        if (Input.GetKeyDown(KeyCode.E) && mitosisGauge != null && mitosisGauge.IsShieldUnlocked()) {
            ToggleShield();
        }

        if(mitosisGauge.IsShieldUnlocked()) {
            UpdateCoilTextUI();
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
    
    void UpdateCoilTextUI() { 
        if (coilText != null && mitosisGauge.currentGauge >= 50.0) { 
            coilText.text = "Coil Ready! Press E to activate!";
        } else if (coilText != null && mitosisGauge.currentGauge <= 49.9) { 
            coilText.text = "Charging coil...";
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
