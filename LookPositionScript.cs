using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPositionScript : MonoBehaviour
{
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(0, Input.GetAxis("Vertical"), 0 * 1f * Time.deltaTime);
        
    }
}
