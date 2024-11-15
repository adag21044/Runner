using UnityEngine;

public class PlayerMovement : Observer
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float forwardSpeed = 5f; // Sürekli ileri hareket hızı
    [SerializeField] private float laneWidth = 2f; // Şerit genişliği
    [SerializeField] private float horizontalSpeed = 10f; // Sağa/Sola hareket hızı
    [SerializeField] private float jumpForce = 2.5f; // Zıplama kuvveti
    private Vector3 targetPosition;
    private Rigidbody rb; // Rigidbody referansı
    private bool isGrounded = true; // Karakterin yere değip değmediğini kontrol eder

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        targetPosition = playerTransform.position;
    }

    private void Update()
    {
        MoveForward();
        MoveHorizontally();
    }

    private void MoveForward()
    {
        Vector3 forwardMovement = new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        playerTransform.position += forwardMovement;
    }

    private void MoveHorizontally()
    {
        playerTransform.position = Vector3.Lerp(
            playerTransform.position,
            new Vector3(targetPosition.x, playerTransform.position.y, playerTransform.position.z),
            horizontalSpeed * Time.deltaTime
        );

        float clampedX = Mathf.Clamp(playerTransform.position.x, -2f, 2f);
        playerTransform.position = new Vector3(clampedX, playerTransform.position.y, playerTransform.position.z);
    }

    public override void OnNotify(NotificationTypes type)
    {
        Debug.Log($"PlayerMovement: {type} bildirimi alındı.");
        switch (type)
        {
            case NotificationTypes.Up:
                Jump();
                break;

            case NotificationTypes.Left:
                if (targetPosition.x > -laneWidth * 2)
                {
                    targetPosition.x -= laneWidth;
                    Debug.Log($"PlayerMovement: Sola hareket (Hedef x: {targetPosition.x})");
                }
                break;

            case NotificationTypes.Right:
                if (targetPosition.x < laneWidth * 2)
                {
                    targetPosition.x += laneWidth;
                    Debug.Log($"PlayerMovement: Sağa hareket (Hedef x: {targetPosition.x})");
                }
                break;
        }
    }

    private void Jump()
    {
        if (isGrounded) // Yerdeyse zıplama işlemini gerçekleştir
        {
            Debug.Log("PlayerMovement: Zıplama tetiklendi.");
            animator.SetTrigger("Jumped"); // Zıplama animasyonunu tetikle
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Yukarı doğru kuvvet uygula
            isGrounded = false; // Karakterin havada olduğunu belirt
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Eğer yere çarparsa karakter yere geri döner
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("PlayerMovement: Karakter yere indi.");
            isGrounded = true;
        }
    }
}
