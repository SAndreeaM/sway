using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public ScoreManager scoreManager;

    public void TriggerGameOver()
    {
        gameOverPanel.SetActive(true);

        int finalScore = scoreManager.GetScore();
        finalScoreText.text = "FINAL SCORE: " + finalScore.ToString();

        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update high score
        if (finalScore > currentHighScore)
        {
            currentHighScore = finalScore;
            PlayerPrefs.SetInt("HighScore", currentHighScore);
            PlayerPrefs.Save();
        }

        highScoreText.text = "HIGHSCORE: " + currentHighScore.ToString();

        Time.timeScale = 0; // Pause the game
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // Resume the game
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}