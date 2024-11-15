using UnityEngine;

public class PlayerMovement : Observer
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveDuration = 0.2f; // Hareketin süresi
    private Vector3 targetPosition;
    private bool isMoving = false;
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        targetPosition = playerTransform.position;
    }

    private void Update()
    {
        if (isMoving)
        {   
            // Player'ın z ekseninde hareket etmesini sağla
            Vector3 currentPosition = playerTransform.position;
            currentPosition.z += moveSpeed * Time.deltaTime;
            playerTransform.position = currentPosition;

            // Pozisyonu smooth bir şekilde güncelle
            playerTransform.position = Vector3.Lerp(playerTransform.position, targetPosition, Time.deltaTime / moveDuration);

            // Hedef pozisyona ulaşıldığında hareketi durdur
            if (Vector3.Distance(playerTransform.position, targetPosition) < 0.01f)
            {
                playerTransform.position = targetPosition; // Pozisyonu tam olarak hedefe eşitle
                isMoving = false; // Hareketi durdur
            }
        }
    }

    public override void OnNotify(NotificationTypes type)
    {
        if (isMoving) return; // Eğer hala hareket ediyorsa başka bir hareket tetiklenmesin

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
                targetPosition = new Vector3(playerTransform.position.x - 1, playerTransform.position.y, playerTransform.position.z);
                animator.SetTrigger("MoveLeft");
                isMoving = true;
                break;
            case NotificationTypes.Right:
                Debug.Log("Player sağa hareket ediyor.");
                targetPosition = new Vector3(playerTransform.position.x + 1, playerTransform.position.y, playerTransform.position.z);
                animator.SetTrigger("MoveRight");
                isMoving = true;
                break;
        }
    }
}
