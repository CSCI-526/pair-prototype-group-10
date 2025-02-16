using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI enemyCountText; 
    private int remainingEnemies = 10; 
    private bool gameOverTriggered = false;
    private Color defaultTextColor; 

    void Start()
    {
        defaultTextColor = enemyCountText.color; 
        UpdateEnemyCountUI(); 
    }

    public void ShowGameOverScreen(bool won)
    {
        if (gameOverTriggered) return;

        gameOverTriggered = true;
        gameOverPanel.SetActive(true);
        enemyCountText.gameObject.SetActive(false); 

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

        remainingEnemies--; 
        UpdateEnemyCountUI(); 

        if (remainingEnemies <= 0)
        {
            ShowGameOverScreen(true); 
        }
    }

    private void UpdateEnemyCountUI()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemies Remaining: " + remainingEnemies;
            enemyCountText.color = defaultTextColor; 
        }
    }

    public void ShowAllianceFormed()
    {
        StopAllCoroutines(); 
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

