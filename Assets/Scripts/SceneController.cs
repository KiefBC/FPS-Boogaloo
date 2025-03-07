using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject iguanaPrefab;
    [SerializeField] private Transform iguanaSpawnPoint;
    
    private GameObject[] enemies;
    private GameObject[] iguanas;
    
    private Vector3 spawnPoint = new(0, 0, 5);
    private int minEnemies = 5;
    private int maxEnemies = 10;
    
    private int minIguanas = 2;
    private int maxIguanas = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);
        int iguanaCount = Random.Range(minIguanas, maxIguanas + 1);

        enemies = new GameObject[enemyCount];
        iguanas = new GameObject[iguanaCount];

        // Spawn enemies
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(i);
        }
    
        // Spawn iguanas
        for (int i = 0; i < iguanaCount; i++)
        {
            SpawnIguana(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check and respawn enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                SpawnEnemy(i);
            }
        }
    
        // Check and respawn iguanas
        for (int i = 0; i < iguanas.Length; i++)
        {
            if (iguanas[i] == null)
            {
                SpawnIguana(i);
            }
        }
    }

    private void SpawnEnemy(int index)
    {
        GameObject enemy = Instantiate(enemyPrefab) as GameObject;
        enemy.transform.position = spawnPoint + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        float angle = Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);
        enemies[index] = enemy;
    }
    
    private void SpawnIguana(int index)
    {
        GameObject iguana = Instantiate(iguanaPrefab) as GameObject;
        iguana.transform.position = iguanaSpawnPoint.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));        float angle = Random.Range(0, 360);
        iguana.transform.Rotate(0, angle, 0);
        iguanas[index] = iguana;
    }
}
