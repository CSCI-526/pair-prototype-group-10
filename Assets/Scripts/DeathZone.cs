using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameOverManager gameOverManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverManager.ShowGameOverScreen(false);
        }
        else if (other.CompareTag("Enemy"))
        {
            SpriteRenderer enemySprite = other.GetComponent<SpriteRenderer>();

            // Check if the enemy is red before counting
            if (enemySprite != null && enemySprite.color == Color.red)
            {
                gameOverManager.DecreaseEnemyCount();
            }

            Destroy(other.gameObject);
        }
    }
}


