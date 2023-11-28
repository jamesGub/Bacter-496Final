using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDetectorScript : MonoBehaviour
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

        Collider2D[] foundItem = Physics2D.OverlapCircleAll(parentBacteria.transform.position, 10);
        //foreach (var x in foundItem) Debug.Log(x.ToString());

        if(foundItem.Length > 5)
        {
            searchIndex = Random.Range(0, 5);
        }
        else
        {
            searchIndex = 0;
        }

        if (foundItem[searchIndex] != null) 
        {
            if (foundItem[searchIndex].CompareTag("AIPlayer") | foundItem[searchIndex].CompareTag("Player"))
            {
                if (prey == null && hunting == true)
                {
                    prey = foundItem[searchIndex];
                    hunting = false;
                }
            }
        }
            if (foundItem[searchIndex].CompareTag("FoodPellet")) 
            { 
                if (detectorTarget == null)
            {
                detectorTarget = foundItem[searchIndex].gameObject.transform;
                searching = false;
            }
            
            
            }
            else
            {
                while (foundItem[searchIndex].CompareTag("AIPlayer") | foundItem[searchIndex].CompareTag("Player"))
                {
                    //Debug.Log("while loop triggered");
                    searchIndex++;
                    if (foundItem.Length < searchIndex)
                    {
                    searchIndex = 0;
                        //break;
                    }
                }
                if (foundItem[searchIndex] != null)
                {
                    if (foundItem[searchIndex].CompareTag("FoodPellet"))
                    {
                        if (detectorTarget == null)
                    {
                        detectorTarget = foundItem[searchIndex].gameObject.transform;
                        searching = false;
                    }

                    }
                }
            }
        
        /**else
        {
            while (foundItem[searchIndex].CompareTag("AIPlayer") | foundItem[searchIndex].CompareTag("Player"))
            {
                Debug.Log("while loop triggered");
                searchIndex++;
                if(foundItem.Length < searchIndex)
                {
                    break;
                }
            }
            if(foundItem[searchIndex] != null)
            {
                if (foundItem[searchIndex].CompareTag("FoodPellet"))
                {
                    detectorTarget = foundItem[searchIndex].gameObject.transform;
                    searching = false;

                }
            }
            
        }**/
        //Debug.Log("member of array is: " + foundItem[searchIndex]);
        //Debug.Log("PFound: " + prey);
        //Debug.Log("DFound: " + detectorTarget);

        parentBacteria.GetComponent<EnemyController>().SetPrey(prey);

        if (prey != null)
        {
            
            if ((prey.transform.position - parentBacteria.transform.position).magnitude > 5)
            {
                prey = null;
                hunting = true;
            }
        }

        if (prey == parentBacteria.GetComponent<CircleCollider2D>())
        {
            prey = null;
        }

        /**else**/ if (detectorTarget != null) {
            //Debug.Log("set target" + detectorTarget); 
            parentBacteria.GetComponent<EnemyController>().SetTarget(detectorTarget);

        }
        else
        {
            Debug.Log("detectorTarget is null");
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
