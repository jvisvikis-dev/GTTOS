using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : Interactable
{
    [SerializeField] private Animator animator;
    public Action turnSign; 
   
    public override void Use()
    {
        animator.SetTrigger("Turn");
        turnSign?.Invoke();
    }
}
