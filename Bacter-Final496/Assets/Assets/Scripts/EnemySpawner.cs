using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Sprite[] enemySprites;
    public int maxEnemies = 10;
    public float spawnAreaWidth = 10f;
    public float spawnAreaHeight = 10f;

    private void Start()
    {
        SpawnInitialEnemies();
    }

    void SpawnInitialEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-spawnAreaWidth, spawnAreaWidth), Random.Range(-spawnAreaHeight, spawnAreaHeight), 0);
        GameObject newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        SpriteRenderer enemyRenderer = newEnemy.GetComponent<SpriteRenderer>();

        if (enemyRenderer == null)
        {
            enemyRenderer = newEnemy.AddComponent<SpriteRenderer>();
        }

        if (enemySprites.Length > 0)
        {
            Sprite randomSprite = enemySprites[Random.Range(0, enemySprites.Length)];

            enemyRenderer.sprite = randomSprite;
            enemyRenderer.sortingLayerName = "Default";
            enemyRenderer.sortingOrder = 0;

            newEnemy.transform.position = new Vector3(newEnemy.transform.position.x, newEnemy.transform.position.y, 0f);
        }
    }
}
