using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static EnemyManager instance;

    [Header("Enemy Limits")]
    [SerializeField] int maxEnemies = 15;

    public static int AliveEnemies { get; private set; }
    public static int MaxEnemies { get; private set; } = 15; // fallback default

    private void Awake() 
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // Copy inspector value to the static global value
        MaxEnemies = maxEnemies;
        // Make sure to start with fresh values each play session
        AliveEnemies = 0;
    }

    public static bool CanSpawn() => AliveEnemies < MaxEnemies;

    //public static void RegisterEnemy() => AliveEnemies++;
    public static void RegisterEnemy()
    {
        AliveEnemies++;
        Debug.Log($"REGISTER -> Alive={AliveEnemies} (Max={MaxEnemies})");
    }
    public static void UnregisterEnemy()
    {
        AliveEnemies--;
        if (AliveEnemies < 0) AliveEnemies = 0;
    }
}

