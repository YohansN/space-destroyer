using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleAsteroid : MonoBehaviour
{

    public float AsteroidLife = 1f;
    //public float AsteroidSpeed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets"))
        {
            DealDamage();
        }
        
    }

    private void DealDamage()
    {
        AsteroidLife--;
        if(AsteroidLife <= 0)
        {
            Destroy(gameObject);
        }
    }

}
