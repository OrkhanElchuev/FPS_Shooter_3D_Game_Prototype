using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles pausing and resuming gameplay via the ESC key.
/// When paused, time is stopped, the cursor is unlocked,
/// and a pause UI container is displayed.
/// The pause system is automatically disabled once the game
/// reaches a terminal state (win or loss) to prevent conflicts
/// with Win/GameOver screens.
/// </summary>

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    [Tooltip("UI container (panel) that is shown when the game is paused.")]
    [SerializeField] GameObject pauseMenuContainer;

    bool isPaused;

    void Start()
    {
        if (pauseMenuContainer != null)
            pauseMenuContainer.SetActive(false);
    }

    void Update()
    {
        // Do not allow pausing once the game has ended (win or loss).
        if (GameManager.IsGameEnded)
            return;
            
        PauseGameOnESCKey();
    }

    private void PauseGameOnESCKey()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        if (isPaused) return;
        isPaused = true;

        if (pauseMenuContainer != null)
            pauseMenuContainer.SetActive(true);

        // Stop gameplay time.
        Time.timeScale = 0f;    
        Time.fixedDeltaTime = 0.02f; 

        // Unlock cursor for UI.
        var inputs = FindFirstObjectByType<StarterAssetsInputs>();
        if (inputs != null) inputs.SetCursorState(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        if (!isPaused) return;
        isPaused = false;

        if (pauseMenuContainer != null)
            pauseMenuContainer.SetActive(false);

        // Resume gameplay time.
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // Lock cursor back for gameplay (StarterAssetsInputs uses CursorLockMode.Locked).
        var inputs = FindFirstObjectByType<StarterAssetsInputs>();
        if (inputs != null) inputs.SetCursorState(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
