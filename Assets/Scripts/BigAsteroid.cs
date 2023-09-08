using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BigAsteroid : MonoBehaviour
{
    public float AsteroidLife = 3f;
    //public float AsteroidSpeed;

    public GameObject[] childAsteroids;
    public float childAsteroidSpeed = 2;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets"))
        {
            //Debug.Log("Vida do asteroid: " + AsteroidLife);
            DealDamage();
        }
    }

    void DealDamage()
    {
        AsteroidLife--;
        if (AsteroidLife <= 0)
        {
            Destroy(gameObject);
            SpawnChildAsteroids();
        }
    }

    void SpawnChildAsteroids()
    {
        foreach(GameObject child in childAsteroids)
        {
            Instantiate(child, transform.position, transform.rotation);
        }
        
    }
}
