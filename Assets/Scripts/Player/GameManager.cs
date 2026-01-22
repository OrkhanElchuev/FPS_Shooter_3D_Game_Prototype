using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game flow controller responsible for displaying enemy count UI,
/// determining win conditions (no enemies and no spawners remaining),
/// and handling restart/quit actions.
/// </summary>

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [Header("References")]
    [Tooltip("UI text displaying enemies alive.")]
    [SerializeField] TMP_Text enemiesLeftText;
    [Tooltip("UI Container shown when the win condition is met.")]
    [SerializeField] GameObject winContainer;

    [Header("Win Settings")]
    [Tooltip("Time scale applied after winning (0 = freeze, 0.2 = slow motion).")]
    [Range(0f, 1f)]
    [SerializeField] float winTimeScale = 0.2f;

    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

    bool hasWon;
    bool hasLost;

    // Number of currently active spawners. SpawnEnemy registers/unregisters.
    public static int ActiveSpawners { get; private set; }
    // True once the game has reached a terminal state (Win or Loss).
    public static bool IsGameEnded { get; private set; }

    void Awake()
    {
        instance = this;
        IsGameEnded = false;
        Time.timeScale = 1f;
    }

    void OnEnable()
    {
        // Subscribe UI to enemy count updates.
        EnemyManager.OnEnemyCountChanged += UpdateEnemiesText;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid calls on destroyed objects.
        EnemyManager.OnEnemyCountChanged -= UpdateEnemiesText;
    }

    void Start()
    {
        // Hide win UI at start.
        winContainer.SetActive(false);

        // Initialize UI display with current enemy count.
        UpdateEnemiesText(EnemyManager.AliveEnemies);
        CheckWinCondition();
    }

    public void TriggerLose()
    {
        if (hasWon || hasLost) return;
        hasLost = true;
        IsGameEnded = true;
    }

    public void TriggerWin()
    {
        if (hasWon || hasLost) return;
        hasWon = true;
        OnWin(); 
    }

    public static void RegisterSpawner()
    {
        ActiveSpawners++;
        instance?.CheckWinCondition();
    }

    public static void UnregisterSpawner()
    {
        ActiveSpawners--;
        ActiveSpawners = Mathf.Max(ActiveSpawners, 0);
        instance?.CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (hasWon || hasLost) return;

        // Win only when no enemies AND no spawners remain.
        if (EnemyManager.AliveEnemies <= 0 && ActiveSpawners <= 0)
        {
            TriggerWin();
        }
    }

    void OnWin()
    {
        IsGameEnded = true;
        hasWon = true;

        if (winContainer != null) winContainer.SetActive(true);

        // Slow down time.
        Time.timeScale = winTimeScale;
        // Physics Consistency after slow down.
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // Unlock cursor so UI buttons can be clicked.
        var inputs = FindFirstObjectByType<StarterAssetsInputs>();
        if (inputs != null) inputs.SetCursorState(false);

        // Unlock cursor for UI.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void UpdateEnemiesText(int aliveEnemies)
    {
        // Update UI with current alive enemy count.
        enemiesLeftText.text = ENEMIES_LEFT_STRING + aliveEnemies;
        CheckWinCondition();
    }

    public void RestartLevelButton()
    {
        // Reset time before reloading
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
