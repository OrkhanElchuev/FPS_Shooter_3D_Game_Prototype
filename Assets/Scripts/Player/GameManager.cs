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
        UpdateEnemiesText(EnemyManager.AliveEnemies);
    }

    void UpdateEnemiesText(int aliveEnemies)
    {
        enemiesLeftText.text = ENEMIES_LEFT_STRING + aliveEnemies;

        if (aliveEnemies <= 0)
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
