using UnityEngine;

public class PlayerMovement : Observer
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveDuration = 0.2f; // Hareketin süresi
    [SerializeField] private float forwardSpeed = 5f; // Sürekli ileri hareket hızı
    [SerializeField] private float laneWidth = 2f; // Şerit genişliği
    private Vector3 targetPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        targetPosition = playerTransform.position;
    }

    private void Update()
    {
        // Sürekli ileri hareket
        Vector3 forwardMovement = new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        playerTransform.position += forwardMovement;

        // Şerit değişikliği için pozisyonu smooth bir şekilde güncelle
        playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, Time.deltaTime / moveDuration);
    }

    public override void OnNotify(NotificationTypes type)
    {
        switch (type)
        {
            case NotificationTypes.Up:
                Debug.Log("Player yukarı hareket ediyor.");
                animator.SetTrigger("MoveUp");
                break;
            case NotificationTypes.Down:
                Debug.Log("Player aşağı hareket ediyor.");
                animator.SetTrigger("MoveDown");
                break;
            case NotificationTypes.Left:
                Debug.Log("Player sola hareket ediyor.");
                if (!IsAtLeftmostLane())
                {
                    targetPosition = new Vector3(targetPosition.x - laneWidth, targetPosition.y, targetPosition.z);
                    animator.SetTrigger("MoveLeft");
                }
                break;
            case NotificationTypes.Right:
                Debug.Log("Player sağa hareket ediyor.");
                if (!IsAtRightmostLane())
                {
                    targetPosition = new Vector3(targetPosition.x + laneWidth, targetPosition.y, targetPosition.z);
                    animator.SetTrigger("MoveRight");
                }
                break;
        }
    }

    // Şerit sınırlarını kontrol et
    private bool IsAtLeftmostLane()
    {
        return targetPosition.x <= -laneWidth; // Sol şerit sınırını belirleyin
    }

    private bool IsAtRightmostLane()
    {
        return targetPosition.x >= laneWidth; // Sağ şerit sınırını belirleyin
    }
}
