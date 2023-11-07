/*using UnityEngine;

public class ToxicCloud : MonoBehaviour
{
    public float healthIncreaseFactor = 2.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.SetToxicCloud(true, healthIncreaseFactor);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.SetToxicCloud(false, 1.0f);
            }
        }
    }
}
*/