using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    #region Properties
    public float asteroidLife;
    public float asteroidLifeTime = 20f;
    public float asteroidSpeed;
    public int asteroidReward;

    public GameObject[] wreckedAsteroids;
    public float wreckedAsteroidSpeed = 5f;

    public Player playerInfo;

    #endregion

    public void Awake()
    {  
        Destroy(gameObject, asteroidLifeTime);
    }


    #region Behave On Collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets") || collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Vida do asteroid: " + AsteroidLife);
            DealDamage();
        }

        if (collision.gameObject.CompareTag("Shield")){
            Debug.Log("REDIRECIONADO");
            Redirection();
        }
    }
    #endregion

    #region Take Damage
    public virtual void DealDamage()
    {
        asteroidLife--;
        if (asteroidLife <= 0)
        {
            ScoreReward(asteroidReward); //Chama m�todo de recompensa -> Adiciona pontos para o player.
            Destroy(gameObject);
            //SpawnChildAsteroids();
        }
    }
    #endregion

    public void Redirection()
    {
        GetComponent<Rigidbody2D>().velocity *= -3;
    }

    public virtual void ScoreReward(int score)
    {
        playerInfo.SetScore(score);
    }

    public void SpawnChildAsteroids()
    {
        foreach (GameObject wreckedAsteroid in wreckedAsteroids)
        {
            //Define a posi��o de spawn dos asteroids filhos
            float y = Random.Range(0f, 0.5f);
            float x = Random.Range(0f, 1f);
            Vector3 wrekedAsteroidPosition = new Vector3(transform.position.x + x, transform.position.y + y, 0); 
            var wrecked = Instantiate(wreckedAsteroid, wrekedAsteroidPosition, transform.rotation);

            //Define a dire��o que os asteroids filhos v�o seguir
            Rigidbody2D rbWreckerd = wrecked.GetComponent<Rigidbody2D>();
            //Pega a dire��o em que o Asteroid Pai estava indo e atribui ao filho.
            var rb = GetComponent<Rigidbody2D>();
            Vector2 randomAdditional = new Vector2(x, y);
            rbWreckerd.velocity = rb.velocity.normalized * wreckedAsteroidSpeed * randomAdditional;

        }
    }

}