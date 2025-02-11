using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public GameOverManager gameOverManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverManager.ShowGameOverScreen(false); // Show "GAME OVER"
        }
        else if (other.CompareTag("Enemy"))
        {
            gameOverManager.IncreaseEnemyDefeated(); // Count fallen enemies
            Destroy(other.gameObject);
        }
    }
}
