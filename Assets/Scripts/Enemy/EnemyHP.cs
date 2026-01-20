using UnityEngine;

[DisallowMultipleComponent]
public class EnemyHP : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject robotExplosionVFX;
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
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void RegisterThisEnemy()
    {
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
