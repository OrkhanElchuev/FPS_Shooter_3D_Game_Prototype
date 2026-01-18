using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] int startHP = 5;

    int currentHP;

    void Awake()
    {
        currentHP = startHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
