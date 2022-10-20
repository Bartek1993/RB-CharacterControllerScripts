using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag == "rigidbody") 
        {
            RigidObjectScript collided_object = other.gameObject.GetComponent<RigidObjectScript>();
            collided_object.boxhealth(10);
            Debug.Log("Collide with box");
                
            
        }
    }
}
