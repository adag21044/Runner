using UnityEngine;

/// <summary>
/// Centralizes game management. Handles player and swipe detector setup.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerCollection playerCollection;

    private void Awake()
    {
        if (swipeDetector == null || player == null)
        {
            Debug.LogError("GameManager: Required components are missing!");
            return;
        }
        
        // Register PlayerCollection as an observer for collectibles
        foreach (Coin coin in FindObjectsOfType<Coin>())
        {
            coin.RegisterObserver(playerCollection);
        }
        // Register the player as an observer of swipe events
        swipeDetector.RegisterObserver(player);
    }
}
