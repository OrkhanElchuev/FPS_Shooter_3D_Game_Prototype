using System;
using UnityEngine;

/// <summary>
/// Global enemy counter and enemy cap controller.
/// Spawners consult CanSpawn(). Enemies call Register / Unregister.
/// Applies count changes for UI element.
/// </summary>

public class EnemyManager : MonoBehaviour
{
    static EnemyManager instance;

    [Header("Enemy Limits")]
    [Tooltip("Maximum number of alive enemies allowed in the scene at once.")]
    [SerializeField] int maxEnemies = 15;

    // Current number of alive enemies counted by the system.
    public static int AliveEnemies { get; private set; }
    // Global enemy cap copied from inspector at runtime, fallback default = 15.
    public static int MaxEnemies { get; private set; } = 15;
    // Event fired when AliveEnemies changes. UI listens to this.
    public static event Action<int> OnEnemyCountChanged;

    private void Awake() 
    {
        // Singleton, only one EnemyManager should exist.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // Copy inspector value to the static global value.
        MaxEnemies = maxEnemies;
        // Make sure to start with fresh values each play session.
        AliveEnemies = 0;
        // Notify UI listeners of initial value.
        OnEnemyCountChanged?.Invoke(AliveEnemies);
    }

    // Return TRUE if a new enemy may be spawned without exceeding the cap.
    public static bool CanSpawn() => AliveEnemies < MaxEnemies;

    // Called by enemies when they are initialized.
    public static void RegisterEnemy()
    {
        AliveEnemies++;
        OnEnemyCountChanged?.Invoke(AliveEnemies);
    }

    // Called when an enemy is destroyed.
    public static void UnregisterEnemy()
    {
        AliveEnemies--;
        // Clamp at 0 for safety.
        if (AliveEnemies < 0) AliveEnemies = 0;
        OnEnemyCountChanged?.Invoke(AliveEnemies);
    }
}

