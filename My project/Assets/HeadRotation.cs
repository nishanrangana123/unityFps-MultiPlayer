using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour
{

    private PlayerAction inputAction;
    public Vector2 mouseRotation;
    public Vector3 headRotation;

   
    private void Awake()
    {

        inputAction = new PlayerAction();

        if(inputAction != null )
        {

            inputAction.character.mouse.performed += e => mouseRotation = e.ReadValue<Vector2>();

            inputAction.Enable();
        }

     
        
    }

    private void Update()
    {
        HeadXRotation();
    }

    private void HeadXRotation()
    {

        headRotation.x += -mouseRotation.y * 10f * Time.deltaTime;
        headRotation.x = Mathf.Clamp(headRotation.x, -60f, 80f);

        transform.localRotation = Quaternion.Euler(headRotation);

    }
}
