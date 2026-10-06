using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public float spawnInterval = 0.5f;
    public float spawnDelay = 0.5f;
    public GameObject enemyPrefab;

    public ObjectPool enemyBulletPool;
    public ObjectPool enemySmokePool;

    public int maxEnemies = 20;
    // Counts living enemies created by this spawner, excluding preplaced enemies.
    public int currentEnemyCount = 0;

    public Transform loc1;
    public Transform loc2;
    public Transform loc3;
    public Transform loc4;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
        {
            return;
        }

        Transform[] spawnLocations = { loc1, loc2, loc3, loc4 };
        int randomIndex = Random.Range(0, spawnLocations.Length);
        Transform spawnLocation = spawnLocations[randomIndex];
        GameObject enemy = Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
        enemy.GetComponentInChildren<EnemyAI>().bulletPool = enemyBulletPool;
        enemy.GetComponentInChildren<EnemyAI>().smokePool = enemySmokePool;
        enemy.GetComponentInChildren<EnemyDeath>().enemySpawnManager = this;

        currentEnemyCount++;
    }

    public void EnemyDied()
    {
        currentEnemyCount--;
    }
}
