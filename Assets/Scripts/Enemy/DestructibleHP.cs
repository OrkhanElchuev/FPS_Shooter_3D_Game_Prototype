using UnityEngine;

/// <summary>
/// Generic health component for any damageable, destroyable object that should NOT
/// affect enemy counters (e.g., turrets, spawners).
/// </summary>

public class DestructibleHP : MonoBehaviour, IDamageable
{
    [Header("References")]
    [Tooltip("Optional VFX prefab spawned when this object is destroyed.")]
    [SerializeField] GameObject explosionVFX;

    [Header("Settings")]
    [Tooltip("Starting health points of this object.")]
    [SerializeField] int startHP = 10;
    
    // Runtime current Health Points.
    int currentHP;

    void Awake()
    {
        // Initialize runtime Health Points from inspector value.
        currentHP = startHP;
    }

    // Called by weapons/explosions and etc. to apply damage to this object.
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0) DestroyThisObject();
    }

    // Handle object's destruction.
    void DestroyThisObject()
    {
        // Spawn destruction VFX if provided.
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
