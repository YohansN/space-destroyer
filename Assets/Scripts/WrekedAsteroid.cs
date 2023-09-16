using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrekedAsteroid : MonoBehaviour
{
    public float wreckedAsteroidLife = 1;
    public float wreckedAsteroidLifeTime = 20f;
    public Player playerInfo;
    public int asteroidReward;

    private void Awake()
    {
        Destroy(gameObject, wreckedAsteroidLifeTime);
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
        {
            ScoreReward(this.asteroidReward);
            Destroy(gameObject);
        }
            

    }

    public void ScoreReward(int score)
    {
        playerInfo.SetScore(score);
    }
}
