using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    private int enemyDefeatedCount = 0;
    public int winThreshold = 10; // Number of enemies needed to win
    private bool gameOverTriggered = false; // Prevents multiple triggers

    public void ShowGameOverScreen(bool won)
    {
        if (gameOverTriggered) return; // Stop if already triggered

        gameOverTriggered = true; // Set flag to prevent further triggers
        gameOverPanel.SetActive(true);
        
        if (won)
        {
            gameOverText.text = "YOU WON!!!";
        }
        else
        {
            gameOverText.text = "GAME OVER!";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseEnemyDefeated()
    {
        if (gameOverTriggered) return; // Don't increase count after game ends

        enemyDefeatedCount++;
        if (enemyDefeatedCount >= winThreshold)
        {
            ShowGameOverScreen(true); // Show "YOU WON!"
        }
    }
}
