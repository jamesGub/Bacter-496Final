using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f; 
    public float currentHealth; 
    public float healthDecreaseRate = 5f; 

    public Slider healthBar; 

    void Start()
    {
        currentHealth = maxHealth; 
    }

    void Update()
    {
        DecreaseHealthOverTime(); 
        UpdateHealthBar(); 
    }

    void DecreaseHealthOverTime()
    {
        currentHealth -= healthDecreaseRate * Time.deltaTime; 
        if (currentHealth <= 0)
        {
            currentHealth = 0; 
            Destroy(gameObject);
            //Application.Quit();  
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth; 
    }

    public void CollectFoodPellet(float healthAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healthAmount); 
    }
}