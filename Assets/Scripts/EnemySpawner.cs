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
        CircleCollider2D collider2D = GetComponent<CircleCollider2D>();
        float colliderRadius = collider2D.radius; //Distância em que os inimigos devem spawnar.

        //Define a posição em que eles serão spawnados.
        Vector2 spawnPosition = colliderRadius * Random.insideUnitCircle.normalized;
        Vector3 a = transform.position + new Vector3(spawnPosition.x, spawnPosition.y, 0);

        //Define aleatoriamente qual inimigo spawnar daquela lista de inimigos.
        int rand2 = Random.Range(0, enemysPrefab2.Length);
        int rand3 = Random.Range(0, enemysPrefab3.Length);

        switch (enemyWaveType)
        {
            case 1:
                Instantiate(enemyPrefab1, a, Quaternion.identity);
                break;
                
            case 2:

                Instantiate(enemysPrefab2[rand2], a, Quaternion.identity);
                break;
                
            case 3:
                Instantiate(enemysPrefab3[rand3], a, Quaternion.identity);
                break;
        }
    }

}
