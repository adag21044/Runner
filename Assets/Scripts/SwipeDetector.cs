using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class SwipeDetector : Subject
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        foreach (var touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                startTouchPosition = touch.screenPosition;
            }
            else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                endTouchPosition = touch.screenPosition;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;
        if (swipeDelta.magnitude > 50)
        {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                Notify(swipeDelta.x > 0 ? NotificationTypes.Right : NotificationTypes.Left);
            }

            if(Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y))
            {
                Notify(swipeDelta.y > 0 ? NotificationTypes.Up : NotificationTypes.Down);
            }
        }
    }
}
