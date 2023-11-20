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

        int searchIndex = 0;

        Collider2D[] foundItem = Physics2D.OverlapCircleAll(parentBacteria.transform.position, 5);
        foreach (var x in foundItem) Debug.Log(x.ToString());

        if (foundItem[searchIndex] != null) { 
            if (foundItem[searchIndex].CompareTag("AIPlayer") | foundItem[0].CompareTag("Player")) { 
                prey = foundItem[searchIndex];
                hunting = false;
            }
            else if (foundItem[searchIndex].CompareTag("FoodPellet")) { 
                detectorTarget = foundItem[searchIndex].gameObject.transform;
                searching = false;
            
            }
        }
       
        Debug.Log("PFound: " + prey);
        Debug.Log("DFound: " + detectorTarget);
        
        if (prey != null)
        {
            parentBacteria.GetComponent<EnemyController>().SetPrey(prey);
            if ((prey.transform.position - parentBacteria.transform.position).magnitude > .5)
            {
                //prey = null;
                hunting = true;
            }
        }
        else if (detectorTarget != null) { 
             parentBacteria.GetComponent<EnemyController>().SetTarget(detectorTarget);

        }
        /*if (detectorTarget == null)
        {
            searching = true;
            searchIndex++;
        }
        else if (prey == null)
        {
            hunting = true;
            searchIndex++;
        }
        */
        

    }

    /*void OnTriggerStay2D(Collider2D other) - testing with professor ! 
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
    }*/

    void NewPrey()
    {
        hunting = true;
    }

}
