using UnityEngine;
using System.Collections;

/// <summary>
/// Handles player movement, collision, and animations.
/// Implements Observer Pattern for input events.
/// </summary>
public class PlayerMovement : Observer
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float forwardSpeed = 5f; // Forward movement speed
    [SerializeField] private float laneWidth = 2f; // Horizontal lane width
    [SerializeField] private float horizontalSpeed = 10f; // Side movement speed
    [SerializeField] private float jumpForce = 0.00f; // Jump force
    [SerializeField] private CameraFollow cameraFollow; // Camera controller reference

    private Vector3 targetPosition;
    private Rigidbody rb; // Rigidbody reference
    private bool isGrounded = true; // Tracks if the player is on the ground
    private bool isGameOver = false; // Tracks if the game has ended
    private bool isHitAnimationPlaying = false; // Tracks if the hit animation is playing

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        targetPosition = playerTransform.position;

        if (cameraFollow == null)
        {
            Debug.LogError("PlayerMovement: Camera reference is missing!");
        }
    }

    private void Update()
    {
        if (isGameOver) return; // Stop all updates if the game is over

        if (isHitAnimationPlaying)
        {
            CheckHitAnimationEnd(); // Ensure hit animation finishes before proceeding
            return;
        }

        MoveForward();
        MoveHorizontally();
    }

    private void MoveForward()
    {
        playerTransform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
    }

    private void MoveHorizontally()
    {
        playerTransform.position = Vector3.Lerp(
            playerTransform.position,
            new Vector3(targetPosition.x, playerTransform.position.y, playerTransform.position.z),
            horizontalSpeed * Time.deltaTime
        );

        float clampedX = Mathf.Clamp(playerTransform.position.x, -laneWidth * 2, laneWidth * 2);
        playerTransform.position = new Vector3(clampedX, playerTransform.position.y, playerTransform.position.z);
    }

    public override void OnNotify(NotificationTypes type)
    {
        if (isGameOver || isHitAnimationPlaying) return;

        switch (type)
        {
            case NotificationTypes.Up:
                Jump();
                break;
            case NotificationTypes.Left:
                MoveLeft();
                break;
            case NotificationTypes.Right:
                MoveRight();
                break;
            case NotificationTypes.Slide:
                Slide();
                break;    
        }
    }

    private void Slide()
    {
        Debug.Log("PlayerMovement: Slide triggered.");

        // Start the Slide coroutine
        StartCoroutine(SlideCoroutine());
    }

    private IEnumerator SlideCoroutine()
    {
        // Save the original rotation
        Quaternion originalRotation = playerTransform.rotation;

        // Rotate the player to 90 degrees on the Y-axis
        Quaternion slideRotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y + 90f, playerTransform.eulerAngles.z);
        playerTransform.rotation = slideRotation; // Apply the slide rotation
        Debug.Log("Player rotated to 90 degrees.");

        // Trigger the Slide animation
        animator.SetTrigger("Slide");

        // Wait for 4 seconds
        yield return new WaitForSeconds(1.15f);

        // Rotate the player back to the original rotation
        playerTransform.rotation = originalRotation; // Restore the original rotation
        Debug.Log("Player rotation reset to original orientation.");
    }






    private void Jump()
    {
        if (isGrounded)
        {
            animator.SetTrigger("Jumped");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void MoveLeft()
    {
        if (targetPosition.x > -laneWidth * 2)
        {
            targetPosition.x -= laneWidth;
        }
    }

    private void MoveRight()
    {
        if (targetPosition.x < laneWidth * 2)
        {
            targetPosition.x += laneWidth;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            animator.SetTrigger("HitToWall");
            isHitAnimationPlaying = true;
        }
    }

    private void CheckHitAnimationEnd()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0: Base Layer
        if (stateInfo.IsName("Hit To Head") && stateInfo.normalizedTime >= 1.0f)
        {
            StopGame();
        }
    }

    private void StopGame()
    {
        isGameOver = true;
        forwardSpeed = 0f;
        horizontalSpeed = 0f;
        animator.speed = 0f;
    }
}
