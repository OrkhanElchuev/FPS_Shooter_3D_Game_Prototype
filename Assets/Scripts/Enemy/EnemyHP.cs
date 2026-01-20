using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] GameObject robotExplosionVFX;
    [SerializeField] float startHP = 5f;

    float currentHP;

    void Awake()
    {
        currentHP = startHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
