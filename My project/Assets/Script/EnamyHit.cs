using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyHit : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;

    public void HealthCalculation(int amount)
    {
        health = health - amount;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
