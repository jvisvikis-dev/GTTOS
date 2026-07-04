using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : Interactable
{
    [SerializeField] private Animator animator;
    [SerializeField] private string interactableUIText;
    public Action turnSign; 
   

    public override void LookingAt()
    {
        UIManager.Instance.SetInteractableText(interactableUIText);
    }
    public override void EndLooking()
    {
        UIManager.Instance.ClearInteractableText();
    }
    public override void Use()
    {
        animator.SetTrigger("Turn");
        turnSign?.Invoke();
    }
}
