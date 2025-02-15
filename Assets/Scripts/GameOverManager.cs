// using UnityEngine;
// using UnityEngine.SceneManagement;
// using TMPro;

// public class GameOverManager : MonoBehaviour
// {
//     public GameObject gameOverPanel;
//     public TextMeshProUGUI gameOverText;
//     private int enemyDefeatedCount = 0;
//     public int winThreshold = 10; // Number of enemies needed to win
//     private bool gameOverTriggered = false; // Prevents multiple triggers

//     public void ShowGameOverScreen(bool won)
//     {
//         if (gameOverTriggered) return; // Stop if already triggered

//         gameOverTriggered = true; // Set flag to prevent further triggers
//         gameOverPanel.SetActive(true);
        
//         if (won)
//         {
//             gameOverText.text = "YOU WON!!!";
//         }
//         else
//         {
//             gameOverText.text = "GAME OVER!";
//         }
//     }

//     public void RestartGame()
//     {
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//     }

//     public void IncreaseEnemyDefeated()
//     {
//         if (gameOverTriggered) return; // Don't increase count after game ends

//         enemyDefeatedCount++;
//         if (enemyDefeatedCount >= winThreshold)
//         {
//             ShowGameOverScreen(true); // Show "YOU WON!"
//         }
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI enemyCountText; // UI text for countdown
    private int remainingEnemies = 10; // Start from 10
    private bool gameOverTriggered = false;
    private Color defaultTextColor; // Stores original text color

    void Start()
    {
        defaultTextColor = enemyCountText.color; // Save default color
        UpdateEnemyCountUI(); // Initialize the UI with 10
    }

    public void ShowGameOverScreen(bool won)
    {
        if (gameOverTriggered) return;

        gameOverTriggered = true;
        gameOverPanel.SetActive(true);
        enemyCountText.gameObject.SetActive(false); // Hide enemy count text

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

    public void DecreaseEnemyCount()
    {
        if (gameOverTriggered || remainingEnemies <= 0) return;

        remainingEnemies--; // Decrease countdown
        UpdateEnemyCountUI(); // Update UI

        if (remainingEnemies <= 0)
        {
            ShowGameOverScreen(true); // Win condition
        }
    }

    private void UpdateEnemyCountUI()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemies Remaining: " + remainingEnemies;
            enemyCountText.color = defaultTextColor; // Reset to default color
        }
    }

    public void ShowAllianceFormed()
    {
        StopAllCoroutines(); // Prevent overlapping color changes
        StartCoroutine(AllianceFormedEffect());
    }

    private IEnumerator AllianceFormedEffect()
    {
        enemyCountText.text = "     Alliance Formed";
        enemyCountText.color = new Color(0.6f, 1.0f, 0.6f);
        yield return new WaitForSeconds(2f); // Display for 2 seconds
        UpdateEnemyCountUI(); // Restore original text
    }
}

