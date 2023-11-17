using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject foodPelletPrefab;
    public GameObject toxicCloudPrefab;
    public int numberOfPellets = 50;
    public int additionalPelletsAfterInterval = 3000;

    public int numberOfClouds = 30;
    public float spawnAreaWidth = 10f;
    public float spawnAreaHeight = 10f;

    [Header("Pellet Textures")]
    public Texture2D redPelletTexture;
    public Texture2D orangePelletTexture;
    public Texture2D yellowPelletTexture;
    public Texture2D greenPelletTexture;
    public Texture2D cyanPelletTexture;
    public Texture2D bluePelletTexture;
    public Texture2D purplePelletTexture;
    private int numberOfTextures = 6;

    void Start()
    {
        SpawnFoodPellets();
        SpawnToxicCoulds();
        StartCoroutine(PelletsOnInterval());
    }

    void SpawnFoodPellets()
    {
        for (int i = 0; i < numberOfPellets; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-spawnAreaWidth, spawnAreaWidth), Random.Range(-spawnAreaHeight, spawnAreaHeight), 0);
            GameObject newPellet = Instantiate(foodPelletPrefab, randomPosition, Quaternion.identity);

            //Randomly selects one of the 6 pellet textures and applies it to the instantiated pellet. This code severely bogged down the load time, so I will look into it again next week. - Andrew
            SpriteRenderer pelletRenderer = newPellet.transform.GetChild(0).GetComponent<SpriteRenderer>();
            int spriteColor = Random.Range(0, numberOfTextures);
            Sprite sprite;
            switch (spriteColor)
            {
                case 0:
                    sprite = Sprite.Create(redPelletTexture, new Rect(0.0f, 0.0f, redPelletTexture.width, redPelletTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    break;
                case 1:
                    sprite = Sprite.Create(orangePelletTexture, new Rect(0.0f, 0.0f, orangePelletTexture.width, orangePelletTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    break;
                case 2:
                    sprite = Sprite.Create(yellowPelletTexture, new Rect(0.0f, 0.0f, yellowPelletTexture.width, yellowPelletTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    break;
                case 3:
                    sprite = Sprite.Create(greenPelletTexture, new Rect(0.0f, 0.0f, greenPelletTexture.width, greenPelletTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    break;
                case 4:
                    sprite = Sprite.Create(cyanPelletTexture, new Rect(0.0f, 0.0f, cyanPelletTexture.width, cyanPelletTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    break;
                case 5:
                    sprite = Sprite.Create(bluePelletTexture, new Rect(0.0f, 0.0f, bluePelletTexture.width, bluePelletTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    break;
                case 6:
                    sprite = Sprite.Create(purplePelletTexture, new Rect(0.0f, 0.0f, purplePelletTexture.width, purplePelletTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    break;
                default:
                    sprite = Sprite.Create(redPelletTexture, new Rect(0.0f, 0.0f, redPelletTexture.width, redPelletTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    break;

            }
            pelletRenderer.sprite = sprite;

            /**
             * original pellet color choice code:
            
            SpriteRenderer pelletRenderer = newPellet.GetComponent<SpriteRenderer>();
            pelletRenderer.color = new Color(Random.value, Random.value, Random.value, 1); 
           
             **/
        }
    }


    void SpawnToxicCoulds() {

        for (int i = 0; i < numberOfClouds; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-spawnAreaWidth, spawnAreaWidth), Random.Range(-spawnAreaHeight, spawnAreaHeight), 0);
            GameObject newPellet = Instantiate(toxicCloudPrefab, randomPosition, Quaternion.identity);


            SpriteRenderer pelletRenderer = newPellet.GetComponent<SpriteRenderer>();
            pelletRenderer.color = new Color(Random.value, Random.value, Random.value, 1);
        }
    }

    IEnumerator PelletsOnInterval() { 
        yield return new WaitForSeconds(10f); 

        for (int i = 0; i < additionalPelletsAfterInterval; i++)
         {
            Vector3 randomPosition = new Vector3(Random.Range(-spawnAreaWidth, spawnAreaWidth), Random.Range(-spawnAreaHeight, spawnAreaHeight), 0);
            GameObject newPellet = Instantiate(foodPelletPrefab, randomPosition, Quaternion.identity); 
         }
    }
}



