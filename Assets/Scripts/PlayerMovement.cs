using UnityEngine;

public class PlayerMovement : Observer
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float forwardSpeed = 5f; // Sürekli ileri hareket hızı
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
        switch (type)
        {
            case NotificationTypes.Left:
                if (targetPosition.x > -laneWidth * 2)
                    targetPosition.x -= laneWidth;
                break;
            case NotificationTypes.Right:
                if (targetPosition.x < laneWidth * 2)
                    targetPosition.x += laneWidth;
                break;
        }
    }
}
