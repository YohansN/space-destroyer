using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleAsteroid : AsteroidBehavior
{
    public override void DealDamage()
    {
        explosionSF.Play();
        asteroidLife--;
        if (asteroidLife <= 0)
        {
            ScoreReward(asteroidReward);
            Destroy(gameObject, 0.05f);
        }
    }
}
