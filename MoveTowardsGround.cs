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
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
        player.transform.localPosition = Vector3.MoveTowards(player.transform.localPosition, ClimbUpDestination.transform.position, Time.deltaTime * 1.55f);
       
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //player.transform.position = new Vector3(ClimbUpDestination.transform.position.x, ClimbUpDestination.transform.position.y, ClimbUpDestination.transform.position.z);
        //rb.isKinematic = false;
       
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
