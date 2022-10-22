using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class MovementRobot : MonoBehaviour
{
    public Rigidbody rigidBody;

    CapsuleCollider capCol;
    [SerializeField]
    public bool is_grounded, is_jumping, is_floating, is_crouching, is_targeting, is_enemy, is_edgeDetected;
    [SerializeField]
    public float x, y, z;
    [SerializeField]
    float rotate;
    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    float _rot;
    [SerializeField]
    Transform lookPosition;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    float jumpSpeed;
    [SerializeField]
    float gravity, player_mass;
    [SerializeField]
    Animator anim;
    [SerializeField]
    float airtime, groundtime;
    [SerializeField]
    public float max_speed;
    [SerializeField]
    PlayableDirector director;
    [SerializeField]
    Vector3 movement;
    [SerializeField]
    EdgeTetector edgeTetector;
    [SerializeField]
    DetectGround detectGround;
    public GameObject[] cameras;
    [SerializeField] GameObject hitboxx;


    private void Start()
    {
        ///GET ANIMATOR
        anim = GetComponent<Animator>();
        //GET RIGIDBODY
        rigidBody = GetComponent<Rigidbody>();
        //GET COLLIDER
        capCol = GetComponent<CapsuleCollider>();

    }

    void Update()
    {


            setInput(); // SETTING PLAYER INPUT
            setAnimator(); // SETTING UP THE ANIMATIONS
            JumpMovementSetup(); // JUMP SETTINGS
            CrouchMovementSet(); // CROUCH MOVEMENT SETTINGS (NOT FULLY IMPLENTED)
            //Targeting(); TARGETING ENEMIES OR OBJECTS (NOT FULLY IMPLENTED )
            CharacterSpeedSetup(); // THIS METHOD ALLOWS ME TO CONTROLL PLAYER SPEED UNDER DIFFERENT CONDITIONS
            GetGroundStatus(); // DETECTING GROUND
            GravityControlls(); // SETTING GRAVITY FOR CHARACTER CONTROLLER
            EdgeDetector(); // INTERACTION WITH THE EDGES
            Attacking(); // PLAYER ATTACKING



    }



    void FixedUpdate()
    {

        GravityControlls(); // CONTROLLING THE GRAVITY
        if (!is_edgeDetected)
            {
                RigidbodyMovement(); // PLAYER MOVEMENT
                RigidBodyJump(); // PLAYER JUMP
            }




    }
    private void Attacking()
    {
        if (is_grounded && !is_edgeDetected && !is_crouching)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetTrigger("attack");
            }
        }
    }


    private void GravityControlls()
    {
        gravity = Physics.gravity.y;
        player_mass = rigidBody.mass;

        if (airtime > 1f && !is_grounded)
        {
            gravity -= 10;
        }

        if (is_grounded)
        {
            gravity = -40f;
        }


    }

    private void GetGroundStatus()
    {
        // CHECKING IF THE PLAYER IS GROUNDED, SCRIPT IS CALLED FROM GROUND DETECTOR OBJECT
        is_grounded = detectGround.is_grounded;
    }


    private void EdgeDetector()
    {
        // CHECKING IF THE PLAYER COLLIDED WITH THE EDGE OBJECT
        is_edgeDetected = edgeTetector.edgeDetected;
    }
    private void setInput()
    {
         // GETTING PLAYER INPUT//
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");


    }

    private void CharacterSpeedSetup()
    {
        //SET SPEED INCREMENT WHEN THE PLAYER IS GROUNDED
        if (is_grounded)
        {
            //speed will slowly increase
            if (movement.magnitude > 0.01)
            {
                speed += 0.25f;
            }
            //speed will slowly dicrease
            if (movement.magnitude < 0.1f)
            {
                speed -= 0.15f;
            }
        }
        ///SET SPEED DECREMENT WHEN THE PLAYER IS NOT GROUNDED
        if (!is_grounded)
        {
            //speed will slowly increase
            if (movement.magnitude > 0.01)
            {
                speed += 0.45f;
            }
            //speed will slowly dicrease
            if (movement.magnitude < 0.01f)
            {
                speed -= 0.25f;
            }
        }
        //SET MAX SPEED ON THE GROUND

        if (is_grounded)
        {
            //if speed value is 6.5f or above, it will lock at 6.5f;
            if (speed >= 7.5f)
            {
                speed = 7.5f;
            }
            //if speed value is 0 or below, it will lock at 0f;
            if (speed <= 0f)
            {
                speed = 0f;
            }
        }

        //SET MAX SPEED FOR CROUCH MOVEMENT
        //NOT FULLY IMPLENTED YET

        if (is_grounded && is_crouching)
        {
            if (speed > 2f)
            {
                speed = 2f;
            }

            if (speed < 0f)
            {
                speed = 0f;
            }
        }
        //SET MAX SPEED WHILE IN THE AIR
        if (!is_grounded)
        {
            //if speed value is 10.5f or above, it will lock at 10.5f;
            if (speed > 10.5f)
            {
                speed = 10.5f;
            }
            //if speed value is 0 or below, it will lock at 0f;
            if (speed < 0f)
            {
                speed = 0f;
            }
        }

    }



    private void CrouchMovementSet()
    {
        if (is_grounded)
        {
            is_crouching = Input.GetKey(KeyCode.C);

        }

        if (!is_grounded)
        {
            is_grounded = false;

        }

    }

    private void JumpMovementSetup()
    {

        if (is_grounded)
        {

            airtime = 0f; // is always 0 while player collides with the ground
            groundtime += 0.005f; // increasing the ground time (some animations and functions wont work while ground time is
                                  // above or below
                                  // a certain value)
            is_floating = false; // player can't float while on the ground
            rigidBody.drag = 0; // rigidbody drag is set to zero
            if (groundtime > 0.5f) // this will only work when the ground time value is over 0.5f
            {
                is_jumping = Input.GetKey(KeyCode.Space);
            }


        }

        if (!is_grounded)
        {
            is_crouching = false; // player will not be able to crouch while in the air
            airtime += 0.005f; // increasing the air time (some animations and functions wont work while ground time is
                               // above or below
                               // a certain value)
            groundtime = 0f; //is always 0 while player is not colliding with the ground
            is_jumping = false; // player cant jump again
            if (airtime > 0.25)
            {
                is_floating = Input.GetKey(KeyCode.Space);
            }
        }
    }

    private void RigidBodyJump()
    {

        rigidBody.AddForce(new Vector3(0f, jumpHeight, 0) * jumpSpeed, ForceMode.Impulse);

        //standard jump when the player dont move
        if (is_grounded && groundtime > 0.25f && is_jumping)
        {
            jumpHeight = 14.5f;
            jumpSpeed = 5;
        }
        //jump from the crouching position, more height
        if (is_grounded && groundtime > 0.5f && is_jumping && is_crouching)
        {
            jumpHeight = 17f;
            jumpSpeed = 5;
        }
        if (!is_grounded)
        {
            jumpHeight = 0f;
            jumpSpeed = 0;

            //when player is in the air and certain conditions are met player can float
            if (!is_grounded && airtime >= 0.25f)
            {
                if (is_floating)
                {

                    rigidBody.drag = 8f; //To float in the air
                }
                else
                {
                    rigidBody.drag = 0f;
                }
            }
        }
    }

    private void RigidbodyMovement()
    {

        // SETTING UP THE CAMERA, I AM USING CINEMACHINE PACKAGE
        // WILL WORK WITH ANY CAMERA TAGGED AS MAIN
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        // RESTRICTION ON CAMERA AXIS, PLAYER WONT ROTATING ON THE Y AXIS
        camForward.y = 0;
        camRight.y = 0;
        // SETTING UP THE MOVEMENT
        // MOVEMENT IS VECTOR3
        movement = camForward * z + camRight * x;
        // IF HORIZONTAL OR VERTICAL INPUT IS GREATER THAN 0.01F THEN PLAYER WILL MOVE AND ROTATE
        if (movement.magnitude > 0.01 && !is_edgeDetected)
        {
            //MOVE RIGIDBODY
            rigidBody.velocity = new Vector3(movement.x * speed, rigidBody.velocity.y, movement.z * speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), .2f);
        }


    }
    //THIS METHOD IS CALLED WHILE PLAYER IS ATTACKING,
    public void showHitBox()
    {
        StartCoroutine("hitboxshow");
    }

    //HIT BOX APPEARS FOR 0.25SEC
    IEnumerator hitboxshow()
    {
        hitboxx.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        hitboxx.SetActive(false);
    }

    // THIS IS CALLED FROM CLIMBING ANIMATION BEHAVIOUR (ANIMATION STATE UPDATE)
    public void setKinematicRB()
    {
        StartCoroutine("setKinematicState");
    }
    public IEnumerator setKinematicState()
    {

        rigidBody.isKinematic = true;
        yield return new WaitForSeconds(0.75f);
        Debug.Log("set kinamatic false");
        rigidBody.isKinematic = false;
    }



    void setAnimator()
    {
        anim.SetFloat("x", x); /// SETTING HORIZONTAL INPUT FOR CHARACTER BLEND TREE
        anim.SetFloat("idlerotation", rotate); //NOT IMPLEMENTED
        anim.SetFloat("z", z); /// SETTING VERTICAL INPUT FOR CHARACTER BLEND TREE
        anim.SetBool("jump", is_jumping); // JUMPING TRUE/FALSE
        anim.SetBool("floating", is_floating); // PLAYER FLOATING ANIMATION TRUE/FALSE
        anim.SetBool("grounded", is_grounded); // WHEN THE PLAYER COLLIDES WITH THE GROUND
        anim.SetFloat("speed", speed); // PLAYER SPEED DIFFERENT PLAYER SPEED = DIFFERENT ANIMATION TRANSITIONS
        anim.SetFloat("groundtime", groundtime); // HOW LONG IS THE PLAYER ON THE GROUND? USED FOR RESTRICTING SOME
                                                 // ANIMATIONS AND MOVEMENT
        anim.SetFloat("airtime", airtime); // HOW LONG IS THE PLAYER ABOVE THE GROUND? USED FOR RESTRICTING SOME
                                           // ANIMATIONS AND MOVEMENT
        anim.SetBool("crouch", is_crouching); // CROUNCHING IS IMPLEMENTED YET ANIMATIONS NOT SET UP
        anim.SetBool("target", is_targeting);   // NOT IMPLEMENTED
        anim.SetBool("climb", is_edgeDetected); // WHEN THE PLAYER COLLIDES WITH THE EDGE
        anim.SetFloat("gravity",gravity);   // GRAVITY
    }






}
