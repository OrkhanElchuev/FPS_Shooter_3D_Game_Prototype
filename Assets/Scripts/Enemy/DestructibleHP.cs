using UnityEngine;

public class DestructibleHP : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] GameObject explosionVFX;

    [Header("Settings")]
    [SerializeField] int startHP = 10;

    int currentHP;

    void Awake()
    {
        currentHP = startHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0) Die();
    }

    void Die()
    {
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
