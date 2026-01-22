using UnityEngine;

[DisallowMultipleComponent]
public class EnemyHP : MonoBehaviour, IDamageable
{
    [Header("References")]
    [Tooltip("Optional VFX prefab spawned when this enemy dies.")]
    [SerializeField] GameObject objectExplosionVFX;

    [Header("Enemy Settings")]
    [Tooltip("Starting health points of this enemy.")]
    [SerializeField] int startHP = 5;
    
    bool registered; // Track whether this enemy was registered with the EnemyManager.
    int currentHP; // Runtime current Health Points.

    void Awake()
    {
        currentHP = startHP;
        RegisterThisEnemy();
    }

    // Called by weapons/explosions and etc. to apply damage to this enemy.
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0) SelfDestruct();
    }

    // Handle Enemy's destruction.
    public void SelfDestruct()
    {
        if (objectExplosionVFX != null)
        {
            Instantiate(objectExplosionVFX, transform.position, Quaternion.identity); 
        }

        Destroy(gameObject);
    }

    // Register this enemy with the global manager so it counts toward the enemy cap.
    void RegisterThisEnemy()
    {
        // Avoid double registering.
        if (registered) return;
        registered = true;
        EnemyManager.RegisterEnemy();
    }

    // Unregister this enemy from the global manager, used in OnDestroy().
    void UnregisterThisEnemy()
    {
        registered = false;
        EnemyManager.UnregisterEnemy();
    }

    void OnDestroy()
    {   
        // Ensure the enemy counter is decremented only once.
        if (!registered) return;
        UnregisterThisEnemy();
    }
}
