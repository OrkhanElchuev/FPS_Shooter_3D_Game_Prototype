using UnityEngine;

[DisallowMultipleComponent]
public class EnemyHP : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] GameObject objectExplosionVFX;

    [Header("Enemy Settings")]
    [SerializeField] int startHP = 5;

    bool registered;
    int currentHP;

    void Awake()
    {
        currentHP = startHP;
        RegisterThisEnemy();
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0) SelfDestruct();
    }

    public void SelfDestruct()
    {
        if (objectExplosionVFX != null)
        {
            Instantiate(objectExplosionVFX, transform.position, Quaternion.identity); 
        }

        Destroy(gameObject);
    }

    void RegisterThisEnemy()
    {
        if (registered) return;
        registered = true;
        EnemyManager.RegisterEnemy();
    }

    void UnregisterThisEnemy()
    {
        registered = false;
        EnemyManager.UnregisterEnemy();
    }

    void OnDestroy()
    {   
        if (!registered) return;
        UnregisterThisEnemy();
    }
}
