using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{ 
    [Header("References")]
    [SerializeField] GameObject robotPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] float spawnTime = 5f;

    PlayerHP player;

    void Start() 
    {
        player = FindFirstObjectByType<PlayerHP>();
        StartCoroutine(SpawnRoutine());
    }

    void Awake()
    {
        GameManager.RegisterSpawner();
    }

    void OnDestroy()
    {
        GameManager.UnregisterSpawner();
    }

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
