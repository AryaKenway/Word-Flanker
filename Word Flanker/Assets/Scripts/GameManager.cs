using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;

    private float timeRemaining;
    private int wordsFound = 0;
    private int targetWords;

    private bool gameEnded = false;

    void Start()
    {
        var level = LevelManager.CurrentLevel;
        if (level == null)
        {
            Debug.LogError("No level loaded!");
            return;
        }

        timeRemaining = level.timeSec;
        targetWords = level.wordCount; 
        UpdateTimerUI();
    }

    void Update()
    {
        if (gameEnded) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0) timeRemaining = 0;

        UpdateTimerUI();

        if (timeRemaining <= 0)
        {
            GameOver();
        }
    }

    public void OnWordFound(string word)
    {
        if (gameEnded) return;

        wordsFound++;
        Debug.Log($"Word found: {word} ({wordsFound}/{targetWords})");

        if (wordsFound >= targetWords)
        {
            LevelComplete();
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();
        }
    }

    private void GameOver()
    {
        gameEnded = true;
        Debug.Log("Game Over");
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    private void LevelComplete()
    {
        gameEnded = true;
        Debug.Log("Level Complete");
        if (levelCompletePanel != null) levelCompletePanel.SetActive(true);
    }

    public void Retry() => LevelManager.RestartLevel();
    public void NextLevel() => LevelManager.NextLevel();
    public void BackToMenu() => SceneManager.LoadScene("MainMenu");
}
