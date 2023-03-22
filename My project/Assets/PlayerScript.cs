using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    [Header("InputAction")]
    private PlayerAction inputAction;

    [Header("Player Rotation")]
    private Vector2 mouseRotation;
    public Vector3 PlayerRotation;

    [Header("Player Movement and Gravity")]
    private Vector2 inputKeybord;
    private CharacterController cc;
    public Vector3 newDirection;
    public float speed;
    public float jumpHeight;

    public float gravity;
    public bool isgrounded;


    [Header(" kepressed activity")]
    public bool isLeftShift;
    public bool isSpacePressed;

    [Header("player animation")]
    [SerializeField]
    private Animator playerAnim;

    private void Awake()
    {
        inputAction = new PlayerAction();

        inputAction.character.mouse.performed += e => mouseRotation = e.ReadValue<Vector2>();
        inputAction.character.move.performed += e => inputKeybord = e.ReadValue<Vector2>();

       


    }

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
       
        KeyPressed();
        PlayerXRotation();
        Gravity();
        PlayerMovement();
        

     
        isgrounded = cc.isGrounded;


        
    }

    
        
    private void Gravity()
    {

        if (!cc.isGrounded)
        {
            newDirection.y += gravity * Time.deltaTime;
        }
        else if (isSpacePressed)
        {
            
            
          newDirection.y -= gravity * jumpHeight * Time.deltaTime;
            


        }  
       
    }

    private void KeyPressed()
    {

        isLeftShift = inputAction.character.Sprint.ReadValue<float>() > 0.1f;
        isSpacePressed = inputAction.character.Jump.ReadValue<float>() > 0.1f;
        
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }





    private void PlayerMovement()
    {
        newDirection.x = inputKeybord.x ;
        newDirection.z = inputKeybord.y ;


        newDirection = transform.TransformDirection(newDirection);

        PlayerSprint();


        cc.Move(newDirection * speed * Time.deltaTime);

        
    }

    private void PlayerSprint()
    {
        if(isLeftShift && cc.isGrounded) 
        {
            speed = 10f;
            playerAnim.SetBool("run", true);
        }
        else if(!isLeftShift && cc.isGrounded) 
        {
            speed = 5f;
            playerAnim.SetBool("run", false);
        }
       
        
    }

    private void PlayerXRotation()
    {
        PlayerRotation.y += 10f * mouseRotation.x * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(PlayerRotation);
      
    }
}
