using Cinemachine;
using TMPro;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] TMP_Text healthText; 

    [Range(1, 100000)]
    [SerializeField] int startHP = 10;

    float currentHP;
    float minHP = 0f;
    int deathVirtualCameraPriority = 20;

    void Awake()
    {
        currentHP = startHP;
        AdjustHealthText();
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount; 
        AdjustHealthText();

        if (currentHP <= 0)
        {
            weaponCamera.parent = null;
            deathVirtualCamera.Priority = deathVirtualCameraPriority;
            Destroy(this.gameObject);
        }
    }

    void AdjustHealthText()
    {
        if (currentHP < 0)
        {
            currentHP = minHP;
        }

        healthText.text = currentHP.ToString();
    }
}


