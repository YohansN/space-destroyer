using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BigAsteroid : AsteroidBehavior
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
