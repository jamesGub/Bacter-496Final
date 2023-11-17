using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{ 
    public float maxHealth = 100f; 
    public float currentHealth; 
    public float healthDecreaseRate = 5f; 
    public float toxicHealthDecreaseRate = 10f; 
    private float regenerationRate = 20f;

    private float regenerationTimer = 0f;

    public Slider healthBar; 
    public GameObject pressPanel;

    private bool toxicEffect = false;
    private bool hasStartedMoving = false;

    public ScreenShake screenShake;

    void Start()
    {
        currentHealth = maxHealth;
        PauseGame();
        screenShake = GetComponentInChildren<ScreenShake>(); 
        
        if (screenShake == null) { 
            screenShake = FindObjectOfType<ScreenShake>();
        }

        if (screenShake == null) { 
            Debug.LogError("ScreenShake script not found."); 
        } else { 
            screenShake.enabled = false;
        }
    }

    void Update()
    {
        if(hasStartedMoving) {
            if (!toxicEffect){
            DecreaseHealthOverTime(healthDecreaseRate);
            } else {
            DecreaseHealthOverTime(toxicHealthDecreaseRate);
            }
            
        }

        if(regenerationTimer > 0) { 
            regenerationTimer -= Time.deltaTime;

            if(regenerationTimer <= 0) { 
                currentHealth += regenerationRate;
                if(currentHealth > maxHealth) { 
                    currentHealth = maxHealth;
                }
                UpdateHealthBar();
            }
        }

        UpdateHealthBar();

        if (screenShake == null) {
            Debug.LogError("ScreenShake script reference is null.");
            return;
        }

         if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            PlayerStartedMoving(); 
            DestroyPressPanel();
            screenShake.enabled = true; 
        }

    }

    void DecreaseHealthOverTime(float healthDecreaseRate)
    {
        if (!toxicEffect) { 
        currentHealth -= healthDecreaseRate * Time.deltaTime; 
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0; 
            //Destroy(gameObject);
            SceneManager.LoadScene("Menu"); 
        }
        
    }


    public void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth; 
    }

     public void SetToxicCloud(bool isAffected)
    {
        toxicEffect = isAffected;
    }

    void PauseGame() { 
        Time.timeScale = 0;
    }

    void StartGame() { 
        Time.timeScale = 1;
    }

    public void PlayerStartedMoving() { 
        hasStartedMoving = true;
    }
    
    public void DestroyPressPanel() { 
        
        if (pressPanel != null) { 
            Destroy(pressPanel);
            StartGame();
        }
    }    
    public void RegenerateHealth() { 
        regenerationTimer = 5f;
    }
    public void CollectFoodPellet(float healthAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healthAmount); 
    }

    public void DamagePlayer(float damageAmount)
    {
        currentHealth -= damageAmount;
    }

}
