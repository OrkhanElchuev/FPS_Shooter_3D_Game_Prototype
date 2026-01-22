using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enemy AI controller that continuously navigates toward the player using
/// NavMeshAgent and self-destructs when reaching the player.
/// </summary>

public class Robot : MonoBehaviour
{
    FirstPersonController player; // Reference to player movement controlled used for chasing.   
    NavMeshAgent agent;

    const string PLAYER_STRING = "Player";

    void Awake()
    {
        // Cache NavMeshAgent used for movement.
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        // Find player controller in scene.
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        // If Player is destroyed, stop chasing.
        if (!player) return;

        // Continuously update destination to player's current position.
        agent.SetDestination(player.transform.position);       
    }

    void OnTriggerEnter(Collider other)
    {
        // When robot reaches player trigger area, self-destruct.
        if (other.CompareTag(PLAYER_STRING))
        {
            EnemyHP enemyHP = GetComponent<EnemyHP>();
            enemyHP.SelfDestruct();
        }
    }
}
