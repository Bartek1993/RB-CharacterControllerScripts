using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    [SerializeField]
    Transform playerPosition;
    [SerializeField]
    Text[] param;
    [SerializeField]
    MovementRobot MovementRobot;
    [SerializeField]
    float speed, move_x, move_y, move_z;
    [SerializeField]
    bool is_grounded, is_jumping, is_floating, is_crouching, is_targeting, is_enemy, is_edgeDetected;
    Vector3 getPlayerPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getPlayerPosition = new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y, playerPosition.transform.position.z);
        is_grounded = MovementRobot.is_grounded;
        is_jumping = MovementRobot.is_jumping;
        is_floating = MovementRobot.is_floating;
        is_crouching = MovementRobot.is_crouching;
        is_targeting = MovementRobot.is_targeting;
        speed = MovementRobot.speed;
        move_x = MovementRobot.x;
        move_z = MovementRobot.z;
        param[0].text = "current speed = " + speed.ToString();
        param[1].text = "Movement axis X = " + move_x.ToString();
        param[2].text = "Movement axis Z = " + move_z.ToString();
        param[3].text = "Ground Detected = " + is_grounded.ToString();
        param[4].text = "Player Jump = " + is_jumping.ToString();
        param[5].text = "Player Floating = " + is_floating.ToString();
        param[6].text = "Player Crouching = " + is_crouching.ToString();
        param[7].text = "Player Aiming = " + is_targeting.ToString();
        param[8].text = "Player Position = " + getPlayerPosition.ToString();

    }
}
