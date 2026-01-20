using Cinemachine;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;

    [SerializeField] float startHP = 10f;

    float currentHP;
    int deathVirtualCameraPriority = 20;

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
            weaponCamera.parent = null;
            deathVirtualCamera.Priority = deathVirtualCameraPriority;
            Destroy(this.gameObject);
        }
    }
}
