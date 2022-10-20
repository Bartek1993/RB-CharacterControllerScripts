using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    [SerializeField] MovementRobot movementRobot;
    [SerializeField] bool isGrounded, hasObject;
    [SerializeField] RaycastHit hit;
    [SerializeField] Ray ray;
    [SerializeField] Transform parentO;
    [SerializeField] Transform getObject;
    [SerializeField] Vector3 camforward, camright;
    [SerializeField]
    bool can_throw;
    [SerializeField] RigidObjectScript objectScript;
    [SerializeField] Rigidbody grabbed_object;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = movementRobot.is_grounded;
        camforward = Camera.main.transform.forward;
        camright = Camera.main.transform.right;
        camforward.y = 0f;
        camright.y = 0f;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.rigidbody)
            {
                objectScript = hit.transform.GetComponent<RigidObjectScript>();
                float distance = objectScript.distance;
                hasObject = objectScript.grabbed;
                if (Input.GetKey(KeyCode.Mouse1) && distance <= 10 && !can_throw && !hasObject)
                {

                    can_throw = true;
                    grabbed_object = objectScript.GetComponent<Rigidbody>();
                    Debug.Log("is Rigid");
                    grabbed_object.AddTorque(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)) * 200f, ForceMode.Force);
                    grabbed_object.isKinematic = true;
                    grabbed_object.drag = 0f;
                    grabbed_object.mass = 10f;
                    grabbed_object.transform.position = Vector3.Slerp(hit.transform.position, parentO.position, 1f);
                    grabbed_object.transform.parent = parentO.transform;

                }

            }
        }  
            
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (can_throw && grabbed_object)
                    {   
                    Debug.Log("is Rigid");
                    grabbed_object.AddTorque(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)) * 200f, ForceMode.Force);
                    grabbed_object.isKinematic = false;
                    grabbed_object.drag = 0f;
                    grabbed_object.mass = 30f;
                    grabbed_object.transform.parent = null;
                    Vector3 throowObcject = camforward * 200f + camright;
                    grabbed_object.AddForce(throowObcject * 300f);
                    can_throw = false;
                    }
                  

                }
                


            
        
    }
}

