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
    public GameObject bacPlayer;
    public GameObject shieldPrefab;
    private GameObject shieldInstance;
    private bool nimbleUnlocked = false;
    private bool nimbleApplied = false;
    private bool eventTriggered = false;
    private bool regenActive= false;
    private bool shieldActive = false;
    private bool shieldUnlocked = false; 
    public bool abilityUnlocked = false;

    void Start()
    {
        currentGauge = 0f; 
        UpdateMitosisBar(); 
        if (mitosisBar != null) {
            UpdateMitosisBar();
        }
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

        if (regenActive) { 
            if (Input.GetKeyDown(KeyCode.R)){ 
                ApplyRegenAbility();
            }
        }

        if (nimbleUnlocked && !nimbleApplied) { 
            ApplyNimble();
        }
    }

    void UpdateMitosisBar()
    {
        if (mitosisBar != null) { 
            mitosisBar.value = currentGauge / maxGauge;
        }
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


    public void ActivateRegenerationAbility() { 
        if (!regenActive) { 
            regenActive = true;
            EventTriggered(); 
        }
    }

    public void ApplyLysosomicAbility()
    {
        if (healthSystem != null) {
            healthSystem.maxHealth += 50; 
            healthSystem.currentHealth += 150;
            healthSystem.UpdateHealthBar(); 
        }

        if (bacPlayer != null) { 
            Vector3 currentScale = bacPlayer.transform.localScale;
            bacPlayer.transform.localScale = new Vector3(currentScale.x * 2, currentScale.y * 2, currentScale.z * 2);
        }
       
        if (playerController != null) {
            playerController.moveSpeed -= 2.0f; 
        }
     
        ResumeGame();

    }

    public void ApplyRegenAbility() { 
        if (healthSystem != null) {
            healthSystem.RegenerateHealth();
        }

        regenActive = false;
        ResumeGame();
    }

    public void UnlockNimble() { 
        nimbleUnlocked = true;
    }

    public void ApplyNimble() { 
        playerController.moveSpeed *= 1.6f;
        transform.localScale *= 0.6f;
        nimbleApplied = true;
        ResumeGame(); 
    }


       public void UnlockShield()
    {
        shieldUnlocked = true;
        ResumeGame();
    }

    public bool IsShieldUnlocked()
    {
        return shieldUnlocked;
    }

    public bool IsShieldActive()
    {
        return shieldActive;
    }

    public void UnlockCollision() { 
        abilityUnlocked = true;
        ResumeGame(); 
    }

    
    public bool UseMitosisGauge(float amount) {
        if (currentGauge >= amount) {
            currentGauge -= amount;
            UpdateMitosisBar();
            return true;
        } else {
            return false;
        }
    }

    public void SetShieldActive(bool active) {
        shieldActive = active; 
    }
    
     public void SelectAbility(int abilityIndex) {
        
        Debug.Log("Ability " + abilityIndex + " selected!");
       
    }

    public void ResumeGame() {
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
