using UnityEngine;
using System.Collections.Generic;

public abstract class Subject : MonoBehaviour
{
    private List<Observer> observers = new List<Observer>();

    [SerializeField] private SubjectTypes subjectType;
    public SubjectTypes SubjectType => subjectType; // Gets the subject type for this instance

    public void RegisterObserver(Observer observer)
    {
        observers.Add(observer); // Adds an observer to the list
    }    

    public void Start()
    {
        ObserverManager.Instance.RegisterSubject(this); // Register this subject with the ObserverManager
    }

    public void Notify(NotificationTypes notificationType)
    {
        // Notify all registered observers of the event
        foreach (var observer in observers)
        {
            observer.OnNotify(notificationType);
        }
    }
}