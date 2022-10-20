using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fracturedmeshscriptbox : MonoBehaviour
{
    //GameObject [] rigidParticles;
    Rigidbody[] rigidbodies;
    BoxCollider[] colliders;
    MeshRenderer[] mesh;
    float alphacollor;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        //alphacollor = 1;
        Destroy(gameObject, 2.2f);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (alphacollor < 0) 
        {
            alphacollor = 0f;
        }

        Invoke(nameof(DisableRigidBodiesAndColliders), 1.5f);
        //Invoke(nameof(SetMeshProperties), 1f);
        colliders = gameObject.GetComponentsInChildren<BoxCollider>();
        rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
        mesh = gameObject.GetComponentsInChildren<MeshRenderer>();
        
        for (i = 0; i < rigidbodies.Length; i++)
        {
            Debug.Log(rigidbodies[i]);
            rigidbodies[i].AddExplosionForce(Random.Range(20, 30), transform.position, Random.Range(5, 10));
            rigidbodies[i].drag = Random.Range(10,20);
        }
        
    }
    void DisableRigidBodiesAndColliders()
    {
        Debug.Log("Invoke Start Disable Rigidbodies and Colliders");
       
        for (int i = 0; i < colliders.Length; i++)
        {
            rigidbodies[i].detectCollisions = false;
            rigidbodies[i].isKinematic = true;
            colliders[i].enabled = false;
        }
    }

    void SetMeshProperties()
    {
        Debug.Log("Invoke Start Set Mesh Properties");
        alphacollor -= 0.0005f;
        for (int i = 0; i < colliders.Length; i++)
        {
            mesh[i].material.color = new Color(0,0,0,alphacollor);
            Debug.Log(mesh[i]);
            
        }
    }
}
