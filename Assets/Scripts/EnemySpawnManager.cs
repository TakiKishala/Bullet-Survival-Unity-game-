using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public float spawnInterval = 0.5f; // Time between spawns in seconds
    public float spawnDelay = 0.5f; // Initial delay before the first spawn
    public GameObject enemyPrefab; // Prefab of the enemy to spawn

    

    public int maxEnemies = 20; // Maximum number of enemies allowed in the scene
    public int currentEnemyCount = 0; // Current number of enemies in the scene

    public Transform loc1;
    public Transform loc2;
    public Transform loc3;
    public Transform loc4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnInterval); // Start spawning enemies at regular intervals
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        

        Transform[] spawnLocations = { loc1, loc2, loc3, loc4 }; // Array of spawn locations
        int randomIndex = Random.Range(0, spawnLocations.Length); // Random index for spawn location
        Transform spawnLocation = spawnLocations[randomIndex]; // Select a random spawn location
        GameObject enemy = Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity); // Create a new enemy instance

        

    }
    
}
