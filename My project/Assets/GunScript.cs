using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;
    private PlayerAction inputAction;

    [SerializeField]
    private Camera mainCamera;
    public float cameraZoom;
    private float cVelocity;
    private float cSmoothTime = 0.3f;

    public bool mouseRight, mouseLeft;

    [Header("UI Data")]
    [SerializeField]
    private GameObject crossAir;

    [Header("Effect")]
    [SerializeField]
    private ParticleSystem muzzel;
    public GameObject woodEffect;


    [Header("fire Weapon and  raycast")]
    private RaycastHit hitInfo;
    public float shootinRange;
    public LayerMask playerLayer;

    private void Awake()
    {
        inputAction = new PlayerAction();
       

        
        
    }
    


    private void OnEnable()
    {
        inputAction.Enable();
    }
    private void OnDisable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState =CursorLockMode.Locked;

     
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseLeft = inputAction.character.fire.ReadValue<float>() > 0.1f;
        mouseRight = inputAction.character.aim.ReadValue<float>() > 0.1f;


        AimWeapon();
        FireWeapon();

    }

    private void FireWeapon()
    {
        if(mouseLeft)
        {
            if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, shootinRange, playerLayer  ))
            {
                GameObject wood = Instantiate(woodEffect, hitInfo.transform.position, Quaternion.LookRotation(hitInfo.normal));
                DestroyObject(wood, 1f);
            }




            playerAnim.SetBool("fire", true);
            muzzel.Play();
           
        }
        else if(!mouseLeft)
        {
            playerAnim.SetBool("fire", false);
            muzzel.Stop();
        }
    }

    private void AimWeapon()
    {

        if (mouseRight)
        {
            playerAnim.SetBool("aim", true);
            cameraZoom = 15f;
            crossAir.SetActive(false);
            

        }
        else if (!mouseRight)
        {
            playerAnim.SetBool("aim", false);
            cameraZoom = 55f;
            crossAir.SetActive(true);

        }


        float newZoomValue = Mathf.SmoothDamp(mainCamera.fieldOfView, cameraZoom, ref cVelocity, cSmoothTime);
        mainCamera.fieldOfView = newZoomValue;
    }

   
}
