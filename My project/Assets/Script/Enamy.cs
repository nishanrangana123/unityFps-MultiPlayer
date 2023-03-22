using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enamy : MonoBehaviour
{

    [Header("Enamy movement")]
    public float enamySpeed = 2f;
    public float giveDamage = 5f;


    [Header("Enamy Things")]
    public NavMeshAgent enamyAgent;
    public Transform playerBody;
    public Transform lookPoint;
    public GameObject shootingRayCastArea;
    public LayerMask player_layer;

    [Header("player Shooting  var")]
    public float timeBtwShooting;
    bool previouslyShoot;



    [Header("Enamy States")]
    public float visionRadius;
    public float shootingRadus;
    public bool playerInVisionRadius;
    public bool playerInShootingRange;
    public bool isPlayer;


    private void Awake()
    {
        enamyAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius,player_layer );
        playerInShootingRange = Physics.CheckSphere(transform.position, shootingRadus, player_layer );

        if(playerInVisionRadius && !playerInShootingRange)
        {
            PursePlayer();
        }

        else if(playerInVisionRadius && playerInShootingRange)
        {
            ShootingPlayer();
        }
    
    }

    private void ShootingPlayer()
    {
        enamyAgent.SetDestination(transform.position);

        transform.LookAt(lookPoint);


        if(!previouslyShoot)
        {
            RaycastHit hit;

            if (Physics.Raycast(shootingRayCastArea.transform.position, shootingRayCastArea.transform.forward, out hit, shootingRadus))
            {
                Debug.Log("Shooting" + hit.transform.name);
            }


        }

        previouslyShoot = true;
       

    }

    private void PursePlayer()
    {
        enamyAgent.SetDestination(playerBody.position);
        

        
    }
}
