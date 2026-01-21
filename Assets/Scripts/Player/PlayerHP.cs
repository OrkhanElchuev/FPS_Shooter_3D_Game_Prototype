using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] TMP_Text healthText; 
    [SerializeField] GameObject gameOverContainer;

    [Header("Player Settings")]
    [Range(1, 1000)]
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
            PlayerGameOver();
        }
    }

    void PlayerGameOver()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = deathVirtualCameraPriority;
        gameOverContainer.SetActive(true);
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);
        Destroy(this.gameObject);
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


