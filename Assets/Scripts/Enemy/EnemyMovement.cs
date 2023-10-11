using UnityEngine;

public class EnemyMovement : MultiStateObjectComponent
{
    public float fallSpeed = 5.0f; // Falling speed
    public float moveSpeed = 2.0f; // Movement speed after landing
    public float moveDistance = 2.0f; // The total distance to move left and right

    private Rigidbody2D rb;
    private bool hasLanded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Initially, disable gravity
    }

    private void Update()
    {
        if (!hasLanded)
        {
            // Simulate falling by applying a downward force
            rb.AddForce(Vector2.down * fallSpeed);

            // Check if the enemy has landed on the ground
            if (IsGrounded())
            {
                hasLanded = true;
                rb.gravityScale = 1; // Enable gravity
            }
        }
        else
        {
            // Calculate the horizontal movement within the moveDistance
            float horizontalInput = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;

            // Move the enemy left and right
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        // Check if the enemy is grounded by casting a ray downward
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                gameObject.SetActive(false);
                break;
            
            case TimeState.Present:
                gameObject.SetActive(true);
                break;
        }
    }
}