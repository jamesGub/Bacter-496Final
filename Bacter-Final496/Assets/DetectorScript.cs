using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorScript : MonoBehaviour
{

    public GameObject parentBacteria;
    public Transform detectorTarget;

    //private ReferencedScript bacteriaController;
    private bool searching = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parentBacteria.GetComponent<EnemyController>().SetTarget(detectorTarget);
        if (detectorTarget == null)
        {
            searching = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("FoodPellet") & searching)
        {
            detectorTarget = other.transform;
            searching = false;
        }
    }

}
