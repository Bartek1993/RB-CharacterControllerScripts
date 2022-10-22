using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticcamerascript : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    Vector3 currentcamPosition, playerPosition, newcamPosition;
    [SerializeField]
    Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        currentcamPosition = transform.position;
        newcamPosition = playerPosition - currentcamPosition - cameraOffset;
        transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
        transform.Translate(newcamPosition * 5f * Time.deltaTime);
    }
}
