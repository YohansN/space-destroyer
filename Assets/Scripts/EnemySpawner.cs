using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //A medida que as fases forem passando o spawnRate deve ir diminuindo!
    //Para mudar o raio em que os inimigos spawnam tem que mexer no radius do collider 2d!!
    private DifficultyController difficultyController;
    //[SerializeField] private float spawnRate = 1;
    [SerializeField] private float spawnRateEasy = 4;
    [SerializeField] private float spawnRateHard = 1;
    private float spawnDelay;
    [SerializeField] private int enemyWaveType; //Trocar para um Enum futuramente. (1, 2, 3)
    [SerializeField] private GameObject levelOne;// Asteroides pequenos
    [SerializeField] private GameObject levelTwo;// Asteroides medios
    [SerializeField] private GameObject[] levelThree;// Asteroides pequenos e médios
    [SerializeField] private GameObject levelFour;// Asteroides grandes
    [SerializeField] private GameObject[] levelFive;// Asteroides pequenos, médios e grandes
    
    [SerializeField] public bool canSpawn = true;

    private void Awake()
    {
        spawnDelay = spawnRateEasy;
    }

    void Start()
    {
        difficultyController = FindObjectOfType<DifficultyController>();
    }

    private void Update()
    {
        spawnDelay -= Time.deltaTime;
        if(spawnDelay < 0)
        {
            Spawner();
            spawnDelay = Mathf.Lerp(spawnRateEasy, spawnRateHard, difficultyController.Difficulty);
        }
    }
    
    private void Spawner()
    {
        if (canSpawn)
        {
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
        int rand3 = Random.Range(0, levelThree.Length);
        int rand5 = Random.Range(0, levelFive.Length);

        if (enemyWaveType == 1)
        {
            //spawnRate = 0.5f;
            GameObject enemy = Instantiate(levelOne, spawnPosition, Quaternion.identity);
            EnemyDirectionOnSpawn(enemy);
        }

        if (enemyWaveType == 2)
        {
            //spawnRate = 0.5f;
            GameObject enemy = Instantiate(levelTwo, spawnPosition, Quaternion.identity);
            EnemyDirectionOnSpawn(enemy);
        }

        else if (enemyWaveType == 3)
        {
            //spawnRate = 0.5f;
            GameObject enemy = Instantiate(levelThree[rand3], spawnPosition, Quaternion.identity);
            EnemyDirectionOnSpawn(enemy);
        }

        if (enemyWaveType == 4)
        {
            //spawnRate = 0.5f;
            GameObject enemy = Instantiate(levelFour, spawnPosition, Quaternion.identity);
            EnemyDirectionOnSpawn(enemy);
        }

        else if (enemyWaveType == 5)
        {
            //spawnRate = 0.5f;
            GameObject enemy = Instantiate(levelFive[rand5], spawnPosition, Quaternion.identity);
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
        //Debug.Log("Posição de mira dos inimigos:" + targetPosition);

        enemyRb.AddForce(normalizedDirection * enemyBehavior.asteroidSpeed);
    }
}
