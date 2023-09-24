using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleAsteroid : AsteroidBehavior
{
    public override void DealDamage()
    {
        asteroidLife--;
        if (asteroidLife <= 0)
        {
            ScoreReward(asteroidReward);
            BeforeDestroy();
            Destroy(gameObject, 0.5f);
        }
        hitSF.Play();
    }
}
