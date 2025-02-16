using UnityEngine;

public class EnemyCircle : MonoBehaviour
{
    public float speed = 2f;
    public bool isAlly = false;
    private SpriteRenderer spriteRenderer;
    private GameOverManager gameOverManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red; 
        gameOverManager = FindObjectOfType<GameOverManager>(); 
    }

    void Update()
    {
        if (!isAlly)
            transform.position += Vector3.left * speed * Time.deltaTime; 
        else
            transform.position += Vector3.right * speed * Time.deltaTime; 
    }

    public void ConvertToAlly()
    {
        isAlly = true;
        spriteRenderer.color = Color.green; // Change color to green when converted to ally
        
        if (gameOverManager != null)
        {
            gameOverManager.ShowAllianceFormed(); 
        }
    }
}


