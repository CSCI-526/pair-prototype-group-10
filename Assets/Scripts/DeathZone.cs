// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class DeathZone : MonoBehaviour
// {
//     public GameOverManager gameOverManager;

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             gameOverManager.ShowGameOverScreen(false); // Show "GAME OVER"
//         }
//         else if (other.CompareTag("Enemy"))
//         {
//             gameOverManager.IncreaseEnemyDefeated(); // Count fallen enemies
//             Destroy(other.gameObject);
//         }
//     }
// }

using UnityEngine;

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
            SpriteRenderer enemySprite = other.GetComponent<SpriteRenderer>();

            // Check if the enemy is red before counting
            if (enemySprite != null && enemySprite.color == Color.red)
            {
                gameOverManager.DecreaseEnemyCount(); // Count only red enemies
            }

            Destroy(other.gameObject); // Remove the enemy from the game
        }
    }
}


