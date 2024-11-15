using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class SwipeDetector : Subject
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        foreach (var touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                startTouchPosition = touch.screenPosition;
                isSwiping = true;
            }
            else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                endTouchPosition = touch.screenPosition;
                isSwiping = false;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        if (swipeDelta.magnitude > 50) // Minimum kaydırma mesafesi
        {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    Debug.Log("Sağa Kaydırma");
                    Notify(NotificationTypes.Right);
                }
                else
                {
                    Debug.Log("Sola Kaydırma");
                    Notify(NotificationTypes.Left);
                }
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    Debug.Log("Yukarı Kaydırma");
                    Notify(NotificationTypes.Up);
                }
                else
                {
                    Debug.Log("Aşağı Kaydırma");
                    Notify(NotificationTypes.Down);
                }
            }
        }
    }
}
