using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidObjectScript : MonoBehaviour
{
    public GameObject fracturedBox;
    Rigidbody rb;
    GameObject player;
    public bool grabbed;
    public float distance;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        grabbed = false;
        health = 10;
    }


    // Update is called once per frame
    void Update()
    {
       
        distance = Vector3.Distance(player.transform.position,transform.position);
        if (grabbed) 
        {
        }

        if (health <= 0)
        {

            GameObject fracturedBoxInst = Instantiate(fracturedBox, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.025f);
        }

    }
    public void boxhealth(int damage) 
    {
        health -= damage;
    }



    private void OnMouseDown()
    {
        
        if (distance <= 10f && Input.GetKey(KeyCode.Mouse1))
        {
            grabbed = !grabbed;
            Debug.Log("grabbed status" + grabbed);
        }

       
    }

    private void OnMouseOver()
    {
       

       
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }
}
