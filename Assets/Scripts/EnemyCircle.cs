// using UnityEngine;

// public class EnemyCircle : MonoBehaviour
// {
//     public float speed = 2f;
//     public bool isAlly = false;

//     void Update()
//     {
//         if (!isAlly)
//             transform.position += Vector3.left * speed * Time.deltaTime; // Move left
//         else
//             transform.position += Vector3.right * speed * Time.deltaTime; // Move right
//     }

//     public void ConvertToAlly()
//     {
//         isAlly = true;
//         GetComponent<SpriteRenderer>().color = Color.green;
//     }
// }

using UnityEngine;

public class EnemyCircle : MonoBehaviour
{
    public float speed = 2f;
    public bool isAlly = false;
    private SpriteRenderer spriteRenderer;
    private GameOverManager gameOverManager; // Reference to GameOverManager

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red; // Set default color to red
        gameOverManager = FindObjectOfType<GameOverManager>(); // Find GameOverManager in scene
    }

    void Update()
    {
        if (!isAlly)
            transform.position += Vector3.left * speed * Time.deltaTime; // Move left
        else
            transform.position += Vector3.right * speed * Time.deltaTime; // Move right
    }

    public void ConvertToAlly()
    {
        isAlly = true;
        spriteRenderer.color = Color.green; // Change color to green when converted to ally
        
        if (gameOverManager != null)
        {
            gameOverManager.ShowAllianceFormed(); // Trigger alliance message
        }
    }
}


