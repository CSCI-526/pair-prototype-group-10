using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 0.05f;
    private bool isGrounded;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        transform.position += new Vector3(move, 0, 0) * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W))
        {
            AddJumpTrigger();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ConvertNearestEnemy();
        }
    }

    void AddJumpTrigger()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false; // Player is now airborne
    }

    void ConvertNearestEnemy()
    {
        EnemyCircle[] enemies = FindObjectsOfType<EnemyCircle>();
        EnemyCircle nearestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (EnemyCircle enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            nearestEnemy.ConvertToAlly();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
