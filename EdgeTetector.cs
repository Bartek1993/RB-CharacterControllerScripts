using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTetector : MonoBehaviour
{
    public bool edgeDetected;

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "edge") 
        {
            edgeDetected = true;
            
        }
    }

     public void OnTriggerExit(Collider other)
    {

        if (other.transform.tag == "edge")
        {
            edgeDetected = false;
            Debug.Log("false");

        }
    }
}
