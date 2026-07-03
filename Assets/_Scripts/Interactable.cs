using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool isClickable;
    public abstract void LookingAt();
    public abstract void EndLooking();
    public abstract void Use();
}
