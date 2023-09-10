using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    #region Properties
    public float asteroidLife;
    public float asteroidLifeTime = 20f;
    public float asteroidSpeed;
    public GameObject targetObject;

    public GameObject[] wreckedAsteroids;
    public float wreckedAsteroidSpeed = 5f;

    #endregion

    public void Awake()
    {  
        Destroy(gameObject, asteroidLifeTime);
    }

    public void Start()
    {
        //Define a diração inicial e movimentação do asteroid para onde o player está.
        var rig = GetComponent<Rigidbody2D>();
        var target = targetObject.transform;
        Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
        var normalizedDirection = (targetPosition - rig.position).normalized;
        rig.AddForce(normalizedDirection * asteroidSpeed);
    }


    #region Take Damage
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
        asteroidLife--;
        if (asteroidLife <= 0)
        {
            Destroy(gameObject);
            //SpawnChildAsteroids();
        }
    }

    public void SpawnChildAsteroids()
    {
        float x = 1f;
        foreach (GameObject wreckedAsteroid in wreckedAsteroids)
        {
            var wrecked = Instantiate(wreckedAsteroid, transform.position * x, transform.rotation);
            x += 0.02f;

            Rigidbody2D rb = wrecked.GetComponent<Rigidbody2D>();
            //Pega a direção em que o Asteroid Pai estava indo e atribui ao filho.
            var target = targetObject.transform;
            Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
            var normalizedDirection = (targetPosition - rb.position).normalized;
            rb.velocity = (normalizedDirection * wreckedAsteroidSpeed);
            
        }
    }
    #endregion

}