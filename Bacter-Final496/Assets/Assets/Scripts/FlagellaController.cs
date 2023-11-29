using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagellaController : MonoBehaviour
{

    public GameObject parentBacteria;
    public float rotationSpeed = 1200;
    private Rigidbody2D bacteriaRB;
    private Vector2 velocity;
    private float angle;
    private Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        bacteriaRB = parentBacteria.GetComponent<Rigidbody2D>();
        prevPos = parentBacteria.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = parentBacteria.transform.position;
        velocity = new Vector2((newPosition.x - prevPos.x), (newPosition.y - prevPos.y));//bacteriaRB.velocity;
                                                                                     //  angle = Mathf.Atan(velocity.y / velocity.x);
                                                                                     //  Vector3 newRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
                                                                                     // transform.eulerAngles = newRotation;
        
        Quaternion toRotation = Quaternion.LookRotation(Vector3.back, velocity);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        prevPos = parentBacteria.transform.position;

    }
}
