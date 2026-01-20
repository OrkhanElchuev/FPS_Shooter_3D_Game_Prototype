using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] float startHP = 10f;

    float currentHP;

    void Awake()
    {
        currentHP = startHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount; 
        
        Debug.Log(amount + " damage taken");

        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
