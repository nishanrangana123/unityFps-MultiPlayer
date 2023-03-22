using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera to Assing")]
    public GameObject AimCam;
    public GameObject AimCanvas;
    public GameObject thirdPersonCamera;
    public GameObject thirdPersonCanvas;


    private void Awake()
    {
        AimCam.SetActive(false);
        AimCanvas.SetActive(false);
        thirdPersonCamera.SetActive(true);
        thirdPersonCanvas.SetActive(true);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
            thirdPersonCamera.SetActive(false);
            thirdPersonCanvas.SetActive(false);

        }


        else if (Input.GetMouseButtonUp(1))
        {
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
            thirdPersonCamera.SetActive(true);
            thirdPersonCanvas.SetActive(true);
        }
    }

  

}
