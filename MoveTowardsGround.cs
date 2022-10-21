  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsGround : StateMachineBehaviour
{
    GameObject player;
    //CapsuleCollider playerCollider;
    GameObject ClimbUpDestination;
    Rigidbody rb;
    MovementRobot movement;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //GET THE player object
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        movement = player.gameObject.GetComponent<MovementRobot>();

        ClimbUpDestination = GameObject.FindGameObjectWithTag("climbpos");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //playerCollider.enabled = false;


        movement.setKinematicRB();
        player.transform.localPosition
        = Vector3.MoveTowards(player.transform.localPosition, ClimbUpDestination.transform.position, Time.deltaTime * 1.35f);

    }
}
