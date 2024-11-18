using UnityEngine;
using System.Collections.Generic;

public class ObserverManager : MonoBehaviour
{
    private static ObserverManager _instance;
    public static ObserverManager Instance => _instance;
    private readonly List<Subject> subjects = new List<Subject>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void RegisterSubject(Subject subject)
    {
        if (!subjects.Contains(subject))
        {
            subjects.Add(subject);
        }
    }

    public void RegisterObserver(Observer observer, SubjectTypes subjectType)
    {
        foreach (var subject in subjects)
        {
            if (subject.SubjectType == subjectType)
            {
                subject.RegisterObserver(observer);
                return;
            }
        }
        Debug.LogWarning("Uygun bir Subject bulunamadı!");
    }
}
