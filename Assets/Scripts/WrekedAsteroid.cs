using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrekedAsteroid : MonoBehaviour
{
    [SerializeField] private float wreckedAsteroidLife = 1;
    [SerializeField] private float wreckedAsteroidLifeTime = 20f;
    [SerializeField] private Player playerInfo;
    [SerializeField] private int asteroidReward;
    [SerializeField] private AudioSource explosionSF;
    [SerializeField] private AudioSource shieldCollisionSF;
    [SerializeField] private ParticleSystem particleExplosion;

    private void Awake()
    {
        Destroy(gameObject, wreckedAsteroidLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets") || collision.gameObject.CompareTag("Player")) 
            DealDamage();

        if (collision.gameObject.CompareTag("Shield"))
        {
            //Debug.Log("REDIRECIONADO");
            shieldCollisionSF.Play();
            Redirection();
        }
    }
    private void Redirection()
    {
        GetComponent<Rigidbody2D>().velocity *= -3;
    }

    private void DealDamage()
    {
        wreckedAsteroidLife--;
        if (wreckedAsteroidLife <= 0)
        {
            ScoreReward(this.asteroidReward);
            BeforeDestroy();
            Destroy(gameObject, 0.5f);
        }
    }

    private void BeforeDestroy()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var collider = GetComponent<Collider2D>();

        spriteRenderer.enabled = false;
        collider.enabled = false;

        explosionSF.Play();
        particleExplosion.Play();
    }

    private void ScoreReward(int score)
    {
        playerInfo.SetScore(score);
    }
}
