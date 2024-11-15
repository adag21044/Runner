using UnityEngine;

public class PlayerMovement : Observer
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerTransform;[SerializeField] private float forwardSpeed = 5f; // Alan (Unity ile serileştirilecek)
    public float ForwardSpeed => forwardSpeed;       // Salt okunur property
    [SerializeField] private float laneWidth = 2f; // Şerit genişliği
    [SerializeField] private float horizontalSpeed = 10f; // Sağa/Sola hareket hızı
    private Vector3 targetPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        targetPosition = playerTransform.position;
    }

    private void Update()
    {
        // Sürekli ileri hareket (z ekseni)
        Vector3 forwardMovement = new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        playerTransform.position += forwardMovement;

        // Sadece yatay eksen (x) için hedef pozisyona doğru hareket
        playerTransform.position = Vector3.Lerp(
            playerTransform.position, 
            new Vector3(targetPosition.x, playerTransform.position.y, playerTransform.position.z), 
            horizontalSpeed * Time.deltaTime
        );

        // Pozisyonu sınırla (x ekseni için)
        float clampedX = Mathf.Clamp(playerTransform.position.x, -2f, 2f); // x sınırlarını -2 ile 2 arasında tut
        playerTransform.position = new Vector3(clampedX, playerTransform.position.y, playerTransform.position.z);
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
