using UnityEngine;
using System.Collections.Generic;

public class ObserverManager : MonoBehaviour
{
    private static ObserverManager _instance = null;
    public static ObserverManager Instance => _instance; // Singleton instance

    private List<Subject> subjects = new List<Subject>();

    private void Awake()
    {
        _instance = this;    
    }

    public void RegisterSubject(Subject subject)
    {
        subjects.Add(subject); // Adds a subject to the list
    }

    public void RegisterObserver(Observer observer, SubjectTypes subjectType)
    {
        // Register the observer with a specific subject type
        foreach (var subject in subjects)
        {
            if (subject.SubjectType == subjectType)
            {
                subject.RegisterObserver(observer);
                return;               
            }
        }
        Debug.LogError("Subject not found"); // Log error if no matching subject is found
    }
}