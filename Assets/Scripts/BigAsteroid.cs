using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BigAsteroid : AsteroidBehavior
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
