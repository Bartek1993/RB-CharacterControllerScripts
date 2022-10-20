using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour
{
    public bool is_grounded;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "ground")
        {
            is_grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "ground")
        {
            is_grounded = false;
        }
    }
}
