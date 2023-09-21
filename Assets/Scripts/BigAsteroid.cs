using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BigAsteroid : AsteroidBehavior
{
    public override void DealDamage()
    {
        hitSF.Play();
        asteroidLife--;
        if (asteroidLife <= 0)
        {
            explosionSF.Play();
            ScoreReward(this.asteroidReward); //Definir na unity
            Destroy(gameObject, 0.2f);
            SpawnChildAsteroids();
        }
    }
}
