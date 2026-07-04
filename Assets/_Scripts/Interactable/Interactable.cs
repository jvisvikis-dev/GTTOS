using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour
{
    public bool isClickable;
    [SerializeField] protected string InteractableUIText;
    public abstract void Use();
    public void LookingAt()
    {
        UIManager.Instance.SetInteractableText(InteractableUIText);
    }
    public void EndLooking()
    {
        UIManager.Instance.ClearInteractableText();
    }
}
