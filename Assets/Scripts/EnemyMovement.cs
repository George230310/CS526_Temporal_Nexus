using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f; // Adjust the speed as needed
    public float distance = 10.0f; // The total distance to move

    private Vector3 leftPosition;
    private Vector3 rightPosition;
    private bool movingRight = true;

    private void Start()
    {
        leftPosition = transform.position - Vector3.right * (distance / 2);
        rightPosition = transform.position + Vector3.right * (distance / 2);
    }

    private void Update()
    {
        Debug.Log("Enemy Position: " + transform.position);

        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPosition, speed * Time.deltaTime);

            if (transform.position.x >= rightPosition.x)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPosition, speed * Time.deltaTime);

            if (transform.position.x <= leftPosition.x)
            {
                movingRight = true;
            }
        }
    }
}