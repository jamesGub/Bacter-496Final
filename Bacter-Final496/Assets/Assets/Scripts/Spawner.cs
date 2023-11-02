using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject foodPelletPrefab; 
    public int numberOfPellets = 50; 
    public float spawnAreaWidth = 10f; 
    public float spawnAreaHeight = 10f; 

    void Start()
    {
        SpawnFoodPellets();
    }

    void SpawnFoodPellets()
    {
        for (int i = 0; i < numberOfPellets; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-spawnAreaWidth, spawnAreaWidth), Random.Range(-spawnAreaHeight, spawnAreaHeight), 0);
            GameObject newPellet = Instantiate(foodPelletPrefab, randomPosition, Quaternion.identity);

            
            SpriteRenderer pelletRenderer = newPellet.GetComponent<SpriteRenderer>();
            pelletRenderer.color = new Color(Random.value, Random.value, Random.value, 1); 
        }
    }
}
