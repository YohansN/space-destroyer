using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float bulletLife = 5;

    void Awake()
    {
        Destroy(gameObject, bulletLife);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        //Destroi o objeto que colidiu com a bala:
        //Destroy(collision.gameObject);
    }

}
