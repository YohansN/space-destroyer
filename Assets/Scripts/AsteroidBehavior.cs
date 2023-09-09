using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public float AsteroidLife;
    //public float AsteroidSpeed;

    public GameObject[] childAsteroids;
    public float childAsteroidSpeed = 2;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets") || collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Vida do asteroid: " + AsteroidLife);
            DealDamage();
        }
    }

    public virtual void DealDamage()
    {
        AsteroidLife--;
        if (AsteroidLife <= 0)
        {
            Destroy(gameObject);
            //SpawnChildAsteroids();
        }
    }

    public void SpawnChildAsteroids()
    {
        foreach (GameObject child in childAsteroids)
        {
            var childAsteroid = Instantiate(child, transform.position, transform.rotation);
        }

    }
}
