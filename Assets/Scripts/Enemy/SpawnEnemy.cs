using System.Collections;
using UnityEngine;

/// <summary>
/// Enemy spawner that periodically attempts to spawn enemies while the player
/// is alive and the global enemy count allows it. Also registers itself with 
/// GameManager so win conditions can depend on spawner destruction.
/// </summary>

public class SpawnEnemy : MonoBehaviour
{ 
    [Header("References")]
    [Tooltip("Enemy prefab to spawn.")]
    [SerializeField] GameObject robotPrefab;
    [Tooltip("Where enemies spawn from.")]
    [SerializeField] Transform spawnPoint;

    [Header("Spawn Settings")]
    [Tooltip("Seconds between spawn attempts.")]
    [SerializeField] float spawnTime = 5f;

    PlayerHP player; // Cached player reference, stop spawning if player is destroyed.

    void Awake()
    {
        // Register this spawner so win condition can require spawners destroyed.
        GameManager.RegisterSpawner();
    }
    
    void Start() 
    {
        player = FindFirstObjectByType<PlayerHP>();
        StartCoroutine(SpawnRoutine());
    }

    void OnDestroy()
    {
        // Unregister spawner on destruction so win condition can complete.
        GameManager.UnregisterSpawner();
    }

    // Periodically attempt to spawn an enemy if under the global cap.
    IEnumerator SpawnRoutine()
    {
        while (player && this)
        {
            if (EnemyManager.CanSpawn())
            {
                Instantiate(robotPrefab, spawnPoint.position, transform.rotation);
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
