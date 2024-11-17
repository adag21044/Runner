using UnityEngine;

/// <summary>
/// Manages the player's state transitions and provides access to components and properties.
/// Implements the State Machine Pattern.
/// </summary>
public class PlayerStateMachine : MonoBehaviour
{
    public IPlayerState CurrentState { get; private set; } // Current state of the player

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 10f;

    public Animator Animator => animator; // Public getter for Animator
    public Rigidbody Rigidbody => rb; // Public getter for Rigidbody
    public float ForwardSpeed
    {
        get => forwardSpeed;
        set => forwardSpeed = value; // Public getter/setter for forward speed
    }
    public float HorizontalSpeed
    {
        get => horizontalSpeed;
        set => horizontalSpeed = value; // Public getter/setter for horizontal speed
    }

    public Vector3 TargetPosition { get; set; } // Target position for horizontal movement
    public bool IsGrounded { get; set; } = true; // Tracks if the player is on the ground

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        TargetPosition = transform.position;

        // Initialize with RunningState
        ChangeState(new RunningState());
    }

    private void Update()
    {
        CurrentState?.UpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CurrentState?.OnCollisionEnter(this, collision);
    }

    public void ChangeState(IPlayerState newState)
    {
        CurrentState = newState;
        CurrentState.EnterState(this);
    }

    public void MoveForward()
    {
        transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
    }

    public void MoveHorizontally()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(TargetPosition.x, transform.position.y, transform.position.z),
            horizontalSpeed * Time.deltaTime
        );
    }
}
