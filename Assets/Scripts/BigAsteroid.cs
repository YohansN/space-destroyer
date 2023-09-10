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
            Destroy(gameObject);
            SpawnChildAsteroids();
        }
    }
}
