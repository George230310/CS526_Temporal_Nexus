using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Speed at which the platform moves.
    [SerializeField] private float moveDistance = 20f; // Distance the platform moves before changing direction.
    
    private Vector3 startPosition;
    private bool movingRight = true; // Determines the direction the platform is moving.

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // Save the initial position.
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the platform's current position based on its direction.
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            if (transform.position.x > startPosition.x + moveDistance)
            {
                movingRight = false; // Change direction if it moved far enough.
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            if (transform.position.x < startPosition.x)
            {
                movingRight = true; // Change direction if it moved back to the start.
            }
        }
    }
}
