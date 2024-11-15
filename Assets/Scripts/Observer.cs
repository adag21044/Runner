using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(NotificationTypes type); // Abstract method for notification response
}