using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour
{
    public bool isClickable;
    [SerializeField] protected string InteractableUIText;
    public abstract void Use();
    public void LookingAt()
    {
        if(isClickable)
            UIManager.Instance.SetInteractableText(InteractableUIText);
        else
            UIManager.Instance.ClearInteractableText();
    }
    public void EndLooking()
    {
        isClickable = true;
        UIManager.Instance.ClearInteractableText();
    }
}
