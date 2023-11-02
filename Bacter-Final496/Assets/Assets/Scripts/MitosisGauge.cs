using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MitosisGauge : MonoBehaviour
{
    public float maxGauge = 100f;
    public float currentGauge;
    public Slider mitosisBar; 
    public HealthSystem healthSystem; 
    public PlayerController playerController;
    public AudioSource LvlEffect;
    public AudioClip Fanfare;
    public GameObject abilityPanel;
    private bool eventTriggered = false; 

    void Start()
    {
        currentGauge = 0f; 
        UpdateMitosisBar(); 
        if (abilityPanel != null) { 
            abilityPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (currentGauge >= maxGauge && !eventTriggered)
        {
            EventTriggered(); 
            eventTriggered = true;
            Time.timeScale = 0; 
        }
    }

    void UpdateMitosisBar()
    {
        mitosisBar.value = currentGauge / maxGauge;
    }

    void EventTriggered()
    {
        if (LvlEffect != null && Fanfare != null) { 
            LvlEffect.PlayOneShot(Fanfare);
        }
        if (abilityPanel != null) {
            abilityPanel.SetActive(true);
            Debug.Log("Event Triggered! Game paused. Click a button to resume.");
        }
    }

    public void ApplyLysosomicAbility()
    {
        if (healthSystem != null) {
            healthSystem.maxHealth += 50; 
            healthSystem.currentHealth += 150;
            healthSystem.UpdateHealthBar(); 
        }

       
        if (playerController != null) {
            playerController.moveSpeed -= 2.0f; 
        }
     
        ResumeGame();

    }

     public void SelectAbility(int abilityIndex) {
        
        Debug.Log("Ability " + abilityIndex + " selected!");
       
    }

    public void ResumeGame()
    {
        currentGauge = 0f;
        UpdateMitosisBar();
        Time.timeScale = 1; 
        eventTriggered = false; 
        if (abilityPanel != null) {
            abilityPanel.SetActive(false); 
        }
    }

    public void CollectFoodPellet(float gaugeAmount)
    {
        currentGauge = Mathf.Min(maxGauge, currentGauge + gaugeAmount);
        UpdateMitosisBar();
    }
}
