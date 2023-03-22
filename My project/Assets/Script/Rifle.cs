using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public Camera cam;
    private float shootRanage = 100f;
    public float damage;
    public float fireCharge = 15f;

    [Header("muzzel flash and partical effect")]
    public ParticleSystem muzzelFlash;
    public GameObject woodEffect;

    [Header("Player referance")]
    public PlayerMovement player;


    [Header("Rifle Ammunination and Shooting")]
    private float nextTimeToShoot = 0f;
    private int maximumAmmunition = 20;
    private int mag = 15;
    private int currentAmmunition;
    private float reloadTime = 1.8f;
    private bool setRealoading = false;

   


    public Animator anim;


    private void Awake()
    {
        currentAmmunition = maximumAmmunition;    
    }


    private void Update()
    {
        if (setRealoading)
        {
            anim.SetBool("reloading", true);
         }

        if(currentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        if(Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1f/fireCharge;
            Shoot();
            anim.SetBool("Fire", true);
            muzzelFlash.Play();


        }
        else if(Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Fire", false);
            muzzelFlash.Stop();

        }
        else if(Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("idleAim", true);
        }
        else if(Input.GetButtonUp("Fire2"))
        {
            anim.SetBool("idleAim", false);
        }
    }


    private void Shoot()
    {

        currentAmmunition--;

        if(currentAmmunition <= 0)
        {
            mag--;
        }

        RaycastHit hitInfo;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootRanage))
        {

            
            EnamyHit gameObj = hitInfo.transform.GetComponent<EnamyHit>();
            GameObject hitObject = Instantiate(woodEffect,hitInfo.transform.position,Quaternion.LookRotation(hitInfo.normal));
            Destroy(hitObject, 1f);

            
            if(gameObj !=null)
            {
                gameObj.HealthCalculation(10);
            }
        }
    }

    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        setRealoading = true;
        Debug.Log("Reloading....");
       

        yield return new WaitForSeconds(reloadTime);

        currentAmmunition = maximumAmmunition;
        player.playerSpeed = 2f;
        setRealoading = false;
        anim.SetBool("reloading", false);
        
    }
}
