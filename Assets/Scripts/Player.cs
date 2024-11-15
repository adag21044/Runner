using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }
}
