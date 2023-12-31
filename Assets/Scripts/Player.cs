using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Properties
    [SerializeField] public int maxHealth = 10;
    [SerializeField] public int currentHealth = 10;
    [SerializeField] private float rotationalSpeed;
    [SerializeField] public float maxImpulse;
    [SerializeField] public float currentImpulse;
    [SerializeField] public float increaseImpulseVariantValue;
    [SerializeField] public float decreaseImpulseVariantValue;
    [SerializeField] private float impulseSpeed;
    private bool impulseIsOn = false;
    private bool isImmune = false;

    [SerializeField] private Rigidbody2D rig;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    //GameLoop
    [SerializeField] private SceneController sceneController;

    //UI
    [SerializeField] public UIHealthController healthBar;
    [SerializeField] private UIScoreController uiScore;
    [SerializeField] private UIImpulseController impulseBar;

    //Sound Effects
    [SerializeField] private AudioSource shootSF;
    [SerializeField] private AudioSource hitSF;
    [SerializeField] private AudioSource PlayerExplosionSF;

    //Bullet properties
    [SerializeField] public float shootCooldown;
    private float nextShootTime = 0f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletFiringSpeed;
    [SerializeField] private Transform bulletSpawnPoint;
    #endregion

    private bool isCallingFrontMove = false;

    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth, currentHealth);
        currentImpulse = maxImpulse;
        impulseBar.SetMaxImpulse(maxImpulse);
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            callFrontMove();
        else
        {
            isCallingFrontMove = false;
            impulseIsOn = false;
            IncreaseImpulse();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextShootTime)
            Shoot();
        if (Input.GetKey(KeyCode.Escape))
            GameEvents.current.PlayerPauseTrigger();
    }

    private void FixedUpdate()
    {
        Rotation();
        if (isCallingFrontMove)
            FrontMove();
    }

    private void FrontMove()
    {
        if (currentImpulse > 0)
        {
            Vector2 forceDirection = transform.up;
            rig.AddForce(forceDirection * impulseSpeed);
            impulseIsOn = true;
            DecreaseImpulse();
        }
    }

    private void callFrontMove() => isCallingFrontMove = true;

    private void Rotation()
    {
        float horizontalMovement = -Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, (horizontalMovement * rotationalSpeed * Time.deltaTime));

        float rotationAngle = transform.rotation.eulerAngles.z;
        float radianAngle = rotationAngle * Mathf.Deg2Rad;

        //Math-magic -> Correct de start angle value of the player
        Vector2 direction = new Vector2(0f, 1f);
        float rotatedX = direction.x * Mathf.Cos(radianAngle) - direction.y * Mathf.Sin(radianAngle);
        float rotatedY = direction.x * Mathf.Sin(radianAngle) + direction.y * Mathf.Cos(radianAngle);

        direction = new Vector2(rotatedX, rotatedY);
        direction.Normalize();
    }

    #region Impulse
    private void DecreaseImpulse() //Chamar quando o jogador se mover.
    {
        if ((currentImpulse > 0) && impulseIsOn)
        {
            currentImpulse -= decreaseImpulseVariantValue * Time.deltaTime;
            impulseBar.SetImpulseBar(currentImpulse);
        }
    }

    private void IncreaseImpulse() //Chamar quando o jogador estiver parado
    {
        if ((currentImpulse < maxImpulse) && !impulseIsOn)
        {
            currentImpulse += increaseImpulseVariantValue * Time.deltaTime;
            impulseBar.SetImpulseBar(currentImpulse);
        }
    }
    #endregion


    private void Shoot()
    {
        shootSF.Play();
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

    private void TakeDamage()
    {
        if (!isImmune)
        {
            hitSF.Play();
            currentHealth--;
            StartCoroutine(DamageIndicator());
            StartCoroutine(ImmunityTime());
            healthBar.SetHealth(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                BeforeDestroy();
                GameEvents.current.PlayerDeathTrigger();
                Destroy(gameObject, 0.5f);
            }
        }
    }

    private void BeforeDestroy()
    {
        Debug.Log("Game Over");
        spriteRenderer.enabled = false;
        collider.enabled = false;
    }

    private IEnumerator ImmunityTime()
    {
        isImmune = true;
        float immuneTime = 1f;
        yield return new WaitForSeconds(immuneTime);
        isImmune = false;
    }

    private IEnumerator DamageIndicator()
    {
        var blinkDuration = 0.1f;
        var originalColor = spriteRenderer.color;

        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(blinkDuration);
        spriteRenderer.color = originalColor;
    }
    #endregion

}