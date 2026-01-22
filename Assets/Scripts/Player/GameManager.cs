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
    [Tooltip("UI element shown when the win condition is met.")]
    [SerializeField] GameObject youWinText;

    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

    // Number of currently active spawners. SpawnEnemy registers/unregisters.
    public static int ActiveSpawners { get; private set; }

    void Awake()
    {
        instance = this;
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
        youWinText.SetActive(false);

        // Initialize UI display with current enemy count.
        UpdateEnemiesText(EnemyManager.AliveEnemies);
        CheckWinCondition();
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
        // Win only when no enemies AND no spawners remain.
        if (EnemyManager.AliveEnemies <= 0 && ActiveSpawners <= 0)
        {
            youWinText.SetActive(true);
        }
    }

    void UpdateEnemiesText(int aliveEnemies)
    {
        // Update UI with current alive enemy count.
        enemiesLeftText.text = ENEMIES_LEFT_STRING + aliveEnemies;
        CheckWinCondition();
    }

    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
