using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Movement")]
    public float playerSpeed;


    [Header("player Animation and Gravity")]
    public CharacterController cc;
    public Animator anim;
    float gravity = -9.81f;

    [Header("Player Camera")]
    public Transform playerCamera;


    [Header("Player Jump and Velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;
    Vector3 movement;
    Vector3 direction;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);





        PlayerMove();
        Jump();
        Gravity();
        SprintPlayer();
        AimHandler();


    }



    private void PlayerMove()
    {
        float horizontal_Axis = Input.GetAxisRaw("Horizontal");
        float vertical_Axis = Input.GetAxisRaw("Vertical");



        direction = new Vector3(horizontal_Axis, 0f, vertical_Axis);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);




            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;




            cc.Move(movement * playerSpeed * Time.deltaTime);


        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            anim.SetBool("jump", true);
          
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else if(Input.GetButtonUp("Jump"))
        {
            anim.SetBool("jump", false);
            
        }

    }

    private void Gravity()
    {
        //gravity
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }


    private void SprintPlayer()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = 10f;
            anim.SetBool("run", true);
        }

        else if (Input.GetKey(KeyCode.W) && Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = 2f;
            anim.SetBool("run", false);

        }
    }
        
       
    private void AimHandler()
    {
        if(direction.x > 0f || direction.z >0f || direction.x < 0f || direction.z < 0f)
        {
         
           
            anim.SetBool("walk", true);
           
        }
        else if(direction.x == 0f || direction.z ==0f)
        {
           
            anim.SetBool("walk", false);
        }
      
        

    }
    
}
