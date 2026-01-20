using UnityEngine;

public class SpawnerHP : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject spawnerExplosionVFX;
    [SerializeField] int startHP = 400;

    int currentHP;

    void Awake()
    {
        currentHP = startHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
            DestroySpawner();
    }

    void DestroySpawner()
    {
        Instantiate(spawnerExplosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
