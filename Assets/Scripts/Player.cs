using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerLife = 10f;
    public float rotationalSpeed;
    public float impulseSpeed;
    public Rigidbody2D rig;

    //Bullet properties
    public GameObject bulletPrefab;
    public float bulletFiringSpeed;
    public Transform bulletSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Player Movimentation
        //Player rotation
        float horizontalMovement = -Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, (horizontalMovement * rotationalSpeed * Time.deltaTime));
        
        float rotationAngle = transform.rotation.eulerAngles.z;
        float radianAngle = rotationAngle * Mathf.Deg2Rad;
        //Debug.Log("Angulo de rotação: " + rotationAngle);

        //Math-magic -> Correct de start angle value of the player
        Vector2 direction = new Vector2(0f, 1f);
        float rotatedX = direction.x * Mathf.Cos(radianAngle) - direction.y * Mathf.Sin(radianAngle);
        float rotatedY = direction.x * Mathf.Sin(radianAngle) + direction.y * Mathf.Cos(radianAngle);
        
        direction = new Vector2(rotatedX, rotatedY);
        direction.Normalize();
        //Debug.Log("Angulo de rotação: " + direction);

        //Player front movimentation
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rig.AddForce(direction * impulseSpeed);
        }

        #endregion

        #region Player Shoot
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation); //Esse codigo pode ser usado para os asteroids
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletFiringSpeed;
        }
        #endregion

        Debug.Log("Vida: " + playerLife);
    }


    #region Player Life
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage();
        }
        
        
    }

    void TakeDamage()
    {
        playerLife--;
        if(playerLife <= 0)
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }
    #endregion

    private void FixedUpdate()
    {

    }
}
