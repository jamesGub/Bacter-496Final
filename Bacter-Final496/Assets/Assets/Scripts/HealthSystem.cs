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

    void Start()
    {
        currentHealth = maxHealth;
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
    
    }

    void DecreaseHealthOverTime(float healthDecreaseRate)
    {
        if (!toxicEffect) { 
        currentHealth -= healthDecreaseRate * Time.deltaTime; 
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0; 
            Destroy(gameObject);
            SceneManager.LoadScene("SampleScene"); 
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

    public void PlayerStartedMoving() { 
        hasStartedMoving = true;
        Destroy(pressPanel);
    }
    
    public void RegenerateHealth() { 
        regenerationTimer = 5f;
    }
    public void CollectFoodPellet(float healthAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healthAmount); 
    }
}
