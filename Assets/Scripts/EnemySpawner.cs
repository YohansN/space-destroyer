using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //A medida que as fases forem passando o spawnRate deve ir diminuindo!
    //Para mudar o raio em que os inimigos spawnam tem que mexer no radius do collider 2d!!
    [SerializeField] private float spawnRate;
    [SerializeField] private int enemyWaveType; //Trocar para um Enum futuramente. (1, 2, 3)
    [SerializeField] private GameObject enemyPrefab1;// Asteroides pequenos
    [SerializeField] private GameObject[] enemysPrefab2;// Asteroides pequenos e médios
    [SerializeField] private GameObject[] enemysPrefab3;// Asteroides pequenos, médios e grandes

    [SerializeField] private bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn)
        {
            yield return wait;
            SpawnWave(enemyWaveType);
        }
    }

    private void SpawnWave(int enemyWaveType)
    {
        #region Enemy Spawn Position
        CircleCollider2D collider2D = GetComponent<CircleCollider2D>();
        float colliderRadius = collider2D.radius; //Distância em que os inimigos devem spawnar.

        //Define a posição em que eles serão spawnados.
        Vector2 position = colliderRadius * Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = transform.position + new Vector3(position.x, position.y, 0);
        
        #endregion
        
        //Define aleatoriamente qual inimigo spawnar daquela lista de inimigos.
        int rand2 = Random.Range(0, enemysPrefab2.Length - 1);
        int rand3 = Random.Range(0, enemysPrefab3.Length - 1);

        if (enemyWaveType == 1)
        {
            GameObject enemy = Instantiate(enemyPrefab1, spawnPosition, Quaternion.identity);
            EnemyDirectionOnSpawn(enemy);
        }
                   
        else if (enemyWaveType == 2)
        {
            GameObject enemy = Instantiate(enemysPrefab2[rand2], spawnPosition, Quaternion.identity);
            EnemyDirectionOnSpawn(enemy);
        }
        else if (enemyWaveType == 3)
        {
            GameObject enemy = Instantiate(enemysPrefab2[rand3], spawnPosition, Quaternion.identity);
            EnemyDirectionOnSpawn(enemy);
        }

    }

    private void EnemyDirectionOnSpawn(GameObject enemy)
    {
        //Define a diração inicial e movimentação do asteroid para onde o player está.
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        var enemyBehavior = enemy.GetComponent<AsteroidBehavior>();

        var playerCurrentPosition = GameObject.FindGameObjectWithTag("Player");
        Vector2 targetPosition = new Vector2(playerCurrentPosition.transform.position.x, playerCurrentPosition.transform.position.y);
        var normalizedDirection = (targetPosition - enemyRb.position).normalized;
        Debug.Log("Posição de mira dos inimigos:" + targetPosition);

        enemyRb.AddForce(normalizedDirection * enemyBehavior.asteroidSpeed);
        Debug.Log("Direção do asterois: " + normalizedDirection * enemyBehavior.asteroidSpeed);
    }
}
