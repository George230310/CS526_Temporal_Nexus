using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float distance = 5.0f;

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
        if(movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if(transform.position.x >=rightPosition.x)
            {
                movingRight = false;
            }
        }

        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if(transform.position.x <= leftPosition.x)
            {
                movingRight = true;
            }
        }

    }

}