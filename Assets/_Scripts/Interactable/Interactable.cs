using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour
{
    public bool isClickable;
    public abstract void Use();
    public abstract void LookingAt();
    public abstract void EndLooking();
}
