using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private PlayerMovement player;

    private void Start()
    {
        ObserverManager.Instance.RegisterSubject(swipeDetector);
        
        // Player'ı SwipeDetector'a bağla
        ObserverManager.Instance.RegisterObserver(player, SubjectTypes.MovementType);
    }
}
