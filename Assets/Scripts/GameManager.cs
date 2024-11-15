using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private PlayerMovement player;

    private void Awake()
    {
        if (swipeDetector == null || player == null)
        {
            Debug.LogError("Gerekli bileşenler atanmadı!");
            return;
        }

        // Swipe hareketlerini dinlemesi için Player'ı SwipeDetector'a bağla
        swipeDetector.RegisterObserver(player);
    }
}
