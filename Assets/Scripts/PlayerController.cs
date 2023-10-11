using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 4.5f;
    public float jumpForce = 5.5f;
    public float gravityScale = 2.0f;
    public Camera mainCamera;

    public Transform Past;
    public Transform Present;

    private bool isPlayerInPresent = true;
    private bool facingRight = true;
    private bool isGrounded = false;
    private Rigidbody2D r2d;
    private CapsuleCollider2D mainCollider;
    private Transform t;

    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
    }

    void Update()
    {
        // Movement controls
        float dirX = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(dirX * maxSpeed, r2d.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        
        TimeTravel();
    }

    void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = IsGrounded();
    }

    private bool IsGrounded()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);

        foreach (var collider in colliders)
        {
            if (collider != mainCollider)
            {
                return true;
            }
        }

        return false;
    }

    private void Jump()
    {
        r2d.velocity = new Vector2(r2d.velocity.x, jumpForce);
    }

    private void TimeTravel()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (isPlayerInPresent)
            {
                // if time manager exists, change global time state
                if (TimeManager.Instance)
                {
                    TimeManager.Instance.ChangeCurrentGlobalTimeState(TimeState.Past);
                }

                isPlayerInPresent = false;
            }
            else
            {
                // if the time manager exists, change global time state
                if (TimeManager.Instance)
                {
                    TimeManager.Instance.ChangeCurrentGlobalTimeState(TimeState.Present);
                }

                isPlayerInPresent = true;
            }
        }
    }
}
