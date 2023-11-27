using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;

    //public MitosisGauge mitosisGauge;

    [Header("Navigation")]
    public string bacteriaClass = "striker";

    public Transform target;
    public Collider2D prey;
    public GameObject origin;

    private Vector2 direction;
    private float horizontalValue = 0;
    private float verticalValue = 0;

    [Header("Health System")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float healthDecreaseRate = 5f;
    public float toxicHealthDecreaseRate = 10f;
    //private float regenerationRate = 20f;

    //private float regenerationTimer = 0f;

    private bool toxicEffect = false;
    public float pelletHealthAmount = 25.0f;

    public float collisionDamage = 20f;

    [Header("Mitosis System")]
    public float maxGauge = 100f;
    public float currentGauge = 0f;
    public float pelletGaugeAmount = 5.0f;
    //private bool eventTriggered = false;
    //private bool regenActive = false;
    public GameObject clonePrefab;
    public GameObject pelletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        target = origin.transform; //new Vector3(0f, 0f, 0f);
        Debug.Log("my class is " + bacteriaClass);
    }

    // Update is called once per frame
    void Update()
    {
        if (toxicEffect)
        {
            DecreaseHealthOverTime(toxicHealthDecreaseRate);
        }
        else
        {
            DecreaseHealthOverTime(healthDecreaseRate);
        }

        if (currentGauge == maxGauge)
        {
            //when mitosis gauge is full, randomly selects a new trait, and activates that trait.
            currentGauge = 0f;
            switch(Random.Range(0, 4))
            {
                case 0:
                    Debug.Log(gameObject.name + " evolved! Lysosomic (0)");
                    ApplyLysosomicAbility();
                    break;
                case 1:
                    Debug.Log(gameObject.name + " evolved! Metabolic (1)");
                    ApplyMetabolicAbility();
                    break;
                case 2:
                    Debug.Log(gameObject.name + " evolved! Flagella (2)");
                    ApplyFlagellaAbility();
                    break;
                case 3:
                    Debug.Log(gameObject.name + " evolved! Coil (3)");
                    ApplyCoilAbility();
                    break;
                default:
                    Debug.Log(gameObject.name + " evolved! Lysosomic (default)");
                    ApplyLysosomicAbility();
                    break;
            }
            //spawn duplicate clonePrefab
            Instantiate(clonePrefab, this.transform.position, Quaternion.identity);
        }

        if (target == null)
        {
            target = origin.transform;
        }

        switch (bacteriaClass)
        {
            case "default":
                DefaultBehavior(); //change to DefaultBehavior() if this is broken
                break;
            case "striker":
                StrikerBehavior();
                break;
            default:
                StrikerBehavior();
                break;
        }
    }

    void DefaultBehavior()
    {
        //horizontalValue = ((1 / (1 + Mathf.Pow(2.71f, -(horizontalValue + Random.Range(-1.0f, 1.0f))))) - 0.5f) * 2;
        //verticalValue = ((1 / ( 1 + Mathf.Pow(2.71f, -(verticalValue + Random.Range(-1.0f, 1.0f))))) - 0.5f) * 2;
        //GameObject.LookAt(target);
        direction.x = target.position.x - this.transform.position.x;
        direction.y = target.position.y - this.transform.position.y;
        direction = Vector2.ClampMagnitude(direction, 1);
        horizontalValue = direction.x;
        verticalValue = direction.y;
        Move(horizontalValue, verticalValue);
    }

    void StrikerBehavior()
    {
        if(prey == null)
        {
            direction.x = target.position.x - this.transform.position.x;
            direction.y = target.position.y - this.transform.position.y;
        }
        else
        {
            direction.x = prey.transform.position.x - this.transform.position.x;
            direction.y = prey.transform.position.y - this.transform.position.y;
        }
        direction = Vector2.ClampMagnitude(direction, 1);
        horizontalValue = direction.x;
        verticalValue = direction.y;
        Move(horizontalValue, verticalValue);

        if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0.01f && gameObject.GetComponent<Rigidbody2D>().velocity.y < 0.01f && gameObject.GetComponent<Rigidbody2D>().velocity.x > -0.01f && gameObject.GetComponent<Rigidbody2D>().velocity.y > -0.01f)
        {
            //Move(0.7f, 0.7f);
        }

    }

    void Move(float horizontal, float vertical)
    {
        Vector3 movement = new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetPrey(Collider2D newPrey)
    {
        prey = newPrey;
    }

    void DecreaseHealthOverTime(float healthDecreaseRate)
    {
        if (!toxicEffect)
        {
            currentHealth -= healthDecreaseRate * Time.deltaTime;
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log(gameObject.name + " died.");
            //convert mitosis gauge to a number of pellets, drop that many pellets in a small area.      OOOOOOOOOOOOO THIS IS A NOTE TO REMEMBER TO IMPLEMENT THIS FEATURE OOOOOOOOOOOOO
            for (int i = 0;i<6;i++)
            {
                Instantiate(pelletPrefab,this.transform.position,Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    public void CollectFoodPellet(float healthAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healthAmount);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FoodPellet"))
        {
        // Haven't decided if this is necessary for the AI players. I figured it might be confusing if the pellet noise plays when the player hasn't picked one up.
        /**    if (BubblePop != null && popN != null)
            {
                float randomPitch = Random.Range(minPitch, maxPitch);
                BubblePop.pitch = randomPitch;
                BubblePop.PlayOneShot(popN);
            }
        **/
            currentHealth = Mathf.Min(maxHealth, currentHealth + pelletHealthAmount);

            currentGauge = Mathf.Min(maxGauge, currentGauge + pelletGaugeAmount);

            Destroy(other.gameObject);
        }else if (other.CompareTag("Player")) //These two else ifs control the collision damage, and they currently don't work as intended.
        {
            Debug.Log("Player collided with AI.");
            other.GetComponent<HealthSystem>().DamagePlayer(collisionDamage);
            currentHealth = currentHealth - other.GetComponent<PlayerController>().DamageOther();

        }else if (other.CompareTag("AIPlayer"))
        {
            Debug.Log("AI collided with each other.");
            other.GetComponent<EnemyController>().Damage(collisionDamage);
            currentHealth = currentHealth - other.GetComponent<EnemyController>().DamageOther();
        }
    }

    void ApplyLysosomicAbility()
    {
            maxHealth += 50;
            currentHealth += 150;
        
            Vector3 currentScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(currentScale.x * 2, currentScale.y * 2, currentScale.z * 2);

            moveSpeed -= moveSpeed*0.6f;

    }

    public void Damage(float damageValue)
    {
        currentHealth = currentHealth - damageValue;
    }

    public float DamageOther()
    {
        return collisionDamage;
    }

    public void ApplyMetabolicAbility()
    {
        moveSpeed *= 1.6f;
        Vector3 currentScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(currentScale.x * 0.6f, currentScale.y * 0.6f, currentScale.z * 0.6f);
    }

    public void ApplyFlagellaAbility()
    {

    }

    public void ApplyCoilAbility()
    {

    }


}



