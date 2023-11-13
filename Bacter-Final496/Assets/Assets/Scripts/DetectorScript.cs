using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorScript : MonoBehaviour
{

    public GameObject parentBacteria;
    public Transform detectorTarget;
    public Collider2D prey = null;

    //private ReferencedScript bacteriaController;
    private bool searching = true;
    private bool hunting = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parentBacteria.GetComponent<EnemyController>().SetTarget(detectorTarget);
        if (prey != null)
        {
            parentBacteria.GetComponent<EnemyController>().SetPrey(prey);
            if ((prey.transform.position - parentBacteria.transform.position).magnitude > 5)
            {
                prey = null;
                hunting = true;
            }
        }
        if (detectorTarget == null)
        {
            searching = true;
        }
        if (prey == null)
        {
            hunting = true;
        }
        

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.CompareTag("AIPlayer") | other.CompareTag("Player")) & hunting)
        {
            prey = other;
            hunting = false;
        }
        if (other.CompareTag("FoodPellet") & searching)
        {
            detectorTarget = other.transform;
            searching = false;
        }
    }

    void NewPrey()
    {
        hunting = true;
    }

}
