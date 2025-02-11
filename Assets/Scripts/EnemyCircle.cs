using UnityEngine;

public class EnemyCircle : MonoBehaviour
{
    public float speed = 2f;
    public bool isAlly = false;

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
        GetComponent<SpriteRenderer>().color = Color.green;
    }
}
