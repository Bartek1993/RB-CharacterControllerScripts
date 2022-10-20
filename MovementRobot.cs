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
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        //switchcam = true;
    }

    void Update()
    {
        
            setControlls();
            setAnimator();
            JumpMovementSetup();
            CrouchMovementSet();
            //Targeting();
            CharacterSpeedSetup();
            GetGroundStatus();
            GravityControlls();
            EdgeDetector();
            GetGroundStatus();
            GravityControlls();
            Attacking();
            
        
      
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

    void FixedUpdate()
    {

        
            if (!is_edgeDetected)
            {
                RigidbodyMovement();
                RigidBodyJump();
            }

            if (is_edgeDetected)
            {
                
            }
        
       
    }

   

    private void GravityControlls()
    {
        gravity = Physics.gravity.y;
        player_mass = rigidBody.mass;

        if (airtime > 1f && !is_grounded) 
        {
            gravity -= 10 * player_mass;
        }

        if (is_grounded) 
        {
            gravity = -40f;
        }


    }

    private void GetGroundStatus()
    {
        is_grounded = detectGround.is_grounded;
    }

 
    private void EdgeDetector()
    {
        is_edgeDetected = edgeTetector.edgeDetected;
    }
    private void setControlls()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        
      
            movement = camForward * z + camRight * x;
          
            //cameras[0].SetActive(true);
            //cameras[1].SetActive(false);
       
    }

    private void CharacterSpeedSetup()
    {
        //SET MAX SPEED ON THE GROUND
        if (is_grounded)
        {
            if (movement.magnitude > 0.01)
            {
                speed += 0.15f;
            }
            if (movement.magnitude < 0.1f)
            {
                speed -= 0.05f;
            }
        }
        ///SET SPEED IN THE AIR;
        if (!is_grounded)
        {
            if (movement.magnitude > 0.01)
            {
                speed += 0.25f;
            }
            if (movement.magnitude < 0.01f)
            {
                speed -= 0.25f;
            }
        }
        //SET MAX SPEED ON THE GROUND
        if (is_grounded)
        {
            if (speed > 6.5f)
            {
                speed = 6.5f;
            }

            if (speed < 0f)
            {
                speed = 0f;
            }
        }

        //SET MAX SPEED FOR CROUCH MOVEMENT
        if (is_grounded && is_crouching)
        {
            if (speed > 2.5f)
            {
                speed = 2.5f;
            }

            if (speed < 0f)
            {
                speed = 0f;
            }
        }
        //SET MAX SPEED WHILE IN THE AIR
        if (!is_grounded)
        {
            if (speed > 10.5f)
            {
                speed = 10.5f;
            }

            if (speed < 0f)
            {
                speed = 9f;
            }
        }

    }

   

    private void CrouchMovementSet()
    {
        if (is_grounded)
        {
            is_crouching = Input.GetKey(KeyCode.LeftControl);

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

            airtime = 0f;
            groundtime += 0.005f;
            is_floating = false;
            rigidBody.drag = 0;
            if (groundtime > 0.5f) 
            {
                is_jumping = Input.GetKey(KeyCode.Space);
            }
            

        }

        if (!is_grounded)
        {
            is_crouching = false;
            airtime += 0.005f;
            groundtime = 0f;
            is_jumping = false;
            if (airtime > 1)
            {
                is_floating = Input.GetKey(KeyCode.Space);
            }
        }
    }

    private void RigidBodyJump()
    {
        
        rigidBody.AddForce(new Vector3(0f, jumpHeight, 0) * jumpSpeed, ForceMode.Impulse);

        if (is_grounded && groundtime > 0.3f && is_jumping)
        {
            jumpHeight = 12.5f;
            jumpSpeed = 5;
        }
        if (is_grounded && groundtime > 0.3f && is_jumping && is_crouching && movement.magnitude < 0.01f)
        {
            jumpHeight = 20f;
            jumpSpeed = 5;
        }
        if (is_grounded && groundtime > 0.3f && is_jumping && is_crouching &&  movement.magnitude > 0.01f)
        {
            jumpHeight = 18f;
            jumpSpeed = 5;
        }
        if (!is_grounded)
        {
            jumpHeight = 0f;
            jumpSpeed = 0;

            if (!is_grounded && airtime >= 0.2f)
            {

                if (is_floating)
                {


                    rigidBody.drag = 8f;
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



        if (movement.magnitude > 0.01 && !is_edgeDetected)
        {
            rigidBody.velocity = new Vector3(movement.x * speed, rigidBody.velocity.y, movement.z * speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), .2f);
        }


    }
    public void showHitBox() 
    {
        StartCoroutine("hitboxshow");
    }

    IEnumerator hitboxshow() 
    {
        hitboxx.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        hitboxx.SetActive(false);
    }

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
        anim.SetFloat("x", x);
        anim.SetFloat("idlerotation", rotate);
        anim.SetFloat("z", z);
        anim.SetBool("jump", is_jumping);
        anim.SetBool("floating", is_floating);
        anim.SetBool("grounded", is_grounded);
        anim.SetFloat("speed", speed);
        anim.SetFloat("groundtime", groundtime);
        anim.SetFloat("airtime", airtime);
        anim.SetBool("crouch", is_crouching);
        anim.SetBool("target", is_targeting);
        anim.SetBool("climb", is_edgeDetected);
        anim.SetFloat("gravity",gravity);
    }

  

    

   
}


