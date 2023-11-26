using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 4.5f;
    public float jumpForce = 5.5f;
    public float gravityScale = 2.0f;
    public Camera mainCamera;

    public List<Checkpoint> Checkpoints;

    public LevelAnalyticsCollect LevelAnalytics;

    public bool tutorialMovementEnabled = true;

    public bool tutorialJumpEnabled = true;

    public bool tutorialTimeTravelEnabled = true;

    private bool isPlayerInPresent = true;
    private bool facingRight = true;
    private bool isGrounded = false;
    private Rigidbody2D r2d;
    private CapsuleCollider2D mainCollider;
    private Transform t;

    [SerializeField] private bool shouldCameraFollow;
    [SerializeField] private GameObject interactionPrompt;

    public TextMeshPro HealthDamageText;

    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        
        if (mainCamera && shouldCameraFollow)
        {
            mainCamera.transform.position = new Vector3(t.position.x, t.position.y, mainCamera.transform.position.z);
        }
        else if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }

        Checkpoints = new();
    }

    void Update()
    {
        // Movement controls
        if (tutorialMovementEnabled)
        {
float dirX = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(dirX * maxSpeed, r2d.velocity.y);
        }
        

        if (tutorialJumpEnabled && Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        
        // Camera follow
        if (mainCamera && shouldCameraFollow)
        {
            mainCamera.transform.position = new Vector3(t.position.x, t.transform.position.y, mainCamera.transform.position.z);
        }
        else if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        
        Interact();
        
        if (tutorialTimeTravelEnabled)
        {
            TimeTravel();
        }
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
            if (collider != mainCollider && !collider.gameObject.CompareTag("Player Foot") && !collider.gameObject.CompareTag("TutorialVolume"))
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

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (TimeManager.Instance && TimeManager.Instance.interactionTarget)
            {
                TimeManager.Instance.interactionTarget.OnInteract();
            }
        }
    }

    public void ToggleInteractionPrompt(bool enable)
    {
        if (interactionPrompt)
        {
            interactionPrompt.SetActive(enable);
        }
    }

    private void TimeTravel()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (LevelAnalytics)
            {
                LevelAnalytics.UpdateTravel();
            }
            
            if (isPlayerInPresent)
            {
                // if time manager exists, change global time state
                if (TimeManager.Instance)
                {
                    TimeManager.Instance.TimeTransition(TimeState.Past);
                }

                isPlayerInPresent = false;
            }
            else
            {
                // if the time manager exists, change global time state
                if (TimeManager.Instance)
                {
                    TimeManager.Instance.TimeTransition(TimeState.Present);
                }

                isPlayerInPresent = true;
            }
        }
    }
}
