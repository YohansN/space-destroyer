using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleAsteroid : AsteroidBehavior
{
    public override void DealDamage()
    {
        AsteroidLife--;
        if (AsteroidLife <= 0)
        {
            Destroy(gameObject);
            SpawnChildAsteroids();
        }
    }
}
