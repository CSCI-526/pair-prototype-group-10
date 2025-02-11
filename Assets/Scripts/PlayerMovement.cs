using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        transform.position += new Vector3(move, 0, 0) * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
        {
            ConvertNearestEnemy();
        }
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
}
