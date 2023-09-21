using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrekedAsteroid : MonoBehaviour
{
    public float wreckedAsteroidLife = 1;
    public float wreckedAsteroidLifeTime = 20f;
    public Player playerInfo;
    public int asteroidReward;
    public AudioSource explosionSF;
    public AudioSource shieldCollisionSF;

    private void Awake()
    {
        Destroy(gameObject, wreckedAsteroidLifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
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
    public void Redirection()
    {
        GetComponent<Rigidbody2D>().velocity *= -3;
    }

    public virtual void DealDamage()
    {
        explosionSF.Play();
        wreckedAsteroidLife--;
        if (wreckedAsteroidLife <= 0)
        {
            ScoreReward(this.asteroidReward);
            Destroy(gameObject, 0.2f);
        }
    }

    public void ScoreReward(int score)
    {
        playerInfo.SetScore(score);
    }
}
