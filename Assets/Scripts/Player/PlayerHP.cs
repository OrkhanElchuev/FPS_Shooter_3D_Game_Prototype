using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the player's health, updates health UI, and handles the
/// game-over state including camera switching, cursor unlocking,
/// and player object cleanup.
/// </summary>

public class PlayerHP : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Virtual camera used when player dies.")]
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [Tooltip("Weapon camera transform that gets detached on death.")]
    [SerializeField] Transform weaponCamera;
    [Tooltip("UI text showing current health.")]
    [SerializeField] TMP_Text healthText; 
    [Tooltip("UI container shown on game over.")]
    [SerializeField] GameObject gameOverContainer;

    [Header("Player Settings")]
    [Tooltip("Starting HP for the player.")]
    [Range(1, 1000)]
    [SerializeField] int startHP = 10;

    float currentHP;
    float minHP = 0f;
    int deathVirtualCameraPriority = 20;

    void Awake()
    {
        // Initialize player health points and update UI.
        currentHP = startHP;
        AdjustHealthText();
    }

    // Called to apply damage to the player.
    public void TakeDamage(int amount)
    {
        currentHP -= amount; 
        AdjustHealthText();

        if (currentHP <= 0)
        {
            PlayerGameOver();
        }
    }

    // Handles death camera switch, UI, cursor unlock and destroys player object.
    void PlayerGameOver()
    {
        // Inform GameManager about Lose Condition.
        FindFirstObjectByType<GameManager>()?.TriggerLose();

        // Detach weapon camera.
        weaponCamera.parent = null;

        // Increase death camera priority to make it active.
        deathVirtualCamera.Priority = deathVirtualCameraPriority;
        gameOverContainer.SetActive(true);
        
        // Unlock cursor for menus.
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        if (starterAssetsInputs != null)
            starterAssetsInputs.SetCursorState(false);

        // Force cursor for Game Over UI.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Destroy(this.gameObject);
    }

    // Clamps health points minimum and updates UI text.
    void AdjustHealthText()
    {
        if (currentHP < 0)
        {
            currentHP = minHP;
        }

        healthText.text = currentHP.ToString();
    }
}


