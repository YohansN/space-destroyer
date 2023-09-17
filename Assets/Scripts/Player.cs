using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Properties
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;
    [SerializeField] private float rotationalSpeed;
    [SerializeField] private float impulseSpeed;
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private UIHealthController healthBar;
    [SerializeField] private UIScoreController uiScore;
    private int initialScore = 0;
    [SerializeField] public int pScore;

    //Bullet properties
    [SerializeField] private float shootCooldown;
    private float nextShootTime = 0f; 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletFiringSpeed;
    [SerializeField] private Transform bulletSpawnPoint;
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        pScore = initialScore;
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
        //Debug.Log("Angulo de rota��o: " + rotationAngle);

        //Math-magic -> Correct de start angle value of the player
        Vector2 direction = new Vector2(0f, 1f);
        float rotatedX = direction.x * Mathf.Cos(radianAngle) - direction.y * Mathf.Sin(radianAngle);
        float rotatedY = direction.x * Mathf.Sin(radianAngle) + direction.y * Mathf.Cos(radianAngle);
        
        direction = new Vector2(rotatedX, rotatedY);
        direction.Normalize();
        //Debug.Log("Angulo de rota��o: " + direction);

        //Player front movimentation
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rig.AddForce(direction * impulseSpeed);
        }

        #endregion

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextShootTime)
            Shoot();
    }
    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletFiringSpeed;
        nextShootTime = Time.time + shootCooldown;
    }


    #region Player Life
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
            TakeDamage();
    }

    private void TakeDamage() //Modificar posteriormente para receber diferentes valores de dano.
    {
        currentHealth--;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }
    #endregion

    #region Score
    public void SetScore(int score)
    {
        this.pScore += score;
        //uiScore.SetScorePoints(this.score);
    }
    #endregion

}
