using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleAsteroid : AsteroidBehavior
{
    public override void DealDamage()
    {
        asteroidLife--;
        if (asteroidLife <= 0)
        {
            ScoreReward(this.asteroidReward); //Definir na unity
            Destroy(gameObject);
            SpawnChildAsteroids();
        }
    }
}
