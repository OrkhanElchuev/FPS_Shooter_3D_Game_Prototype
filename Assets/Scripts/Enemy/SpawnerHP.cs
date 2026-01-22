using UnityEngine;

/// <summary>
/// Health component for enemy spawners. Allows spawners to be destroyed
/// without affecting enemy count directly.
/// </summary>

public class SpawnerHP : MonoBehaviour, IDamageable
{
    [Header("References")]
    [Tooltip("VFX prefab spawned when spawner is destroyed.")]
    [SerializeField] GameObject spawnerExplosionVFX;

    [Header("Spawner Health Settings")]
    [Tooltip("Starting health points of the spawner.")]
    [SerializeField] int startHP = 400;

    int currentHP;

    void Awake()
    {
        // Initialize runtime Health Points from inspector value.
        currentHP = startHP;
    }

    // Called by weapons to apply damage to this spawner.
    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        if (currentHP <= 0) DestroySpawner();
    }

    // Handle spawner destruction.
    void DestroySpawner()
    {
        if (spawnerExplosionVFX != null)
        {
            Instantiate(spawnerExplosionVFX, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
