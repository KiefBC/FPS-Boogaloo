using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject[] enemies;
    private Vector3 spawnPoint = new(0, 0, 5);
    private int minEnemies = 5;
    private int maxEnemies = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);
        enemies = new GameObject[enemyCount];
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                SpawnEnemy(i);
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
}
