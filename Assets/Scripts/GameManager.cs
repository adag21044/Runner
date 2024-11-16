using UnityEngine;

/// <summary>
/// Centralizes game management. Handles player and swipe detector setup.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private PlayerMovement player;

    private void Awake()
    {
        if (swipeDetector == null || player == null)
        {
            Debug.LogError("GameManager: Required components are missing!");
            return;
        }

        // Register the player as an observer of swipe events
        swipeDetector.RegisterObserver(player);
    }
}
