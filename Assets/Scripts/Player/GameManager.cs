using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] GameObject youWinText;

    int enemiesLeft = 0;

    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

    public static int ActiveSpawners { get; private set; }

    void OnEnable()
    {
        EnemyManager.OnEnemyCountChanged += UpdateEnemiesText;
    }

    void OnDisable()
    {
        EnemyManager.OnEnemyCountChanged -= UpdateEnemiesText;
    }

    void Start()
    {
        youWinText.SetActive(false);
        UpdateEnemiesText(EnemyManager.AliveEnemies);
    }

    public static void RegisterSpawner()
    {
        ActiveSpawners++;
    }

    public static void UnregisterSpawner()
    {
        ActiveSpawners--;
        ActiveSpawners = Mathf.Max(ActiveSpawners, 0);
    }

    void UpdateEnemiesText(int aliveEnemies)
    {
        enemiesLeftText.text = ENEMIES_LEFT_STRING + aliveEnemies;

        if (aliveEnemies <= 0 && ActiveSpawners <= 0)
        {
            youWinText.SetActive(true);
        }
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
