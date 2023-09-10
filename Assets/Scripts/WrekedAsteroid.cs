using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrekedAsteroid : MonoBehaviour
{
    public float wreckedAsteroidLife = 1;
    public float wreckedAsteroidLifeTime = 20f;

    private void Awake()
    {
        Destroy(gameObject, wreckedAsteroidLifeTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets") || collision.gameObject.CompareTag("Player")) 
            DealDamage();
    }

    public virtual void DealDamage()
    {
        wreckedAsteroidLife--;
        if (wreckedAsteroidLife <= 0)
            Destroy(gameObject);
    }
}
