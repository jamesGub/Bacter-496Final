using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public MitosisGauge mitosisGauge;

    public string bacteriaClass = "default";

    public Transform target;

    private Vector2 direction;
    private float horizontalValue = 0;
    private float verticalValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        target.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        switch(bacteriaClass)
        {
            case "default":
                DefaultBehavior();
                break;
            default:
                DefaultBehavior();
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
    
    void Move(float horizontal, float vertical)
    {
        Vector3 movement = new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
