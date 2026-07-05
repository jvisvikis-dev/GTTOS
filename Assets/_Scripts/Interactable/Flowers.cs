using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flowers : Interactable
{
    [SerializeField] private CinemachineCamera flowerCam;
    [SerializeField] private Animator animator;
    private Controls _inputActions;
    public Action smelledRoses;
    private void Awake()
    {
        ToggleFlowerCam(false);
        _inputActions = new Controls();
        _inputActions.Player.CallMum.performed += SmellFlowers;
        _inputActions.Player.Drop.performed += StopLooking;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Disable();
    }
    private void SmellFlowers(InputAction.CallbackContext context)
    {
        if (!isClickable)
        {
            animator.SetTrigger("Smell");
            smelledRoses?.Invoke();
        }
    }
    private void StopLooking(InputAction.CallbackContext context)
    {
        ToggleFlowerCam(false);
        isClickable = true;
        UIManager.Instance.CloseControls();
    }
    public override void Use()
    {
        isClickable = false;
        ToggleFlowerCam(true);
        UIManager.Instance.OpenControls("Smell");
    }

    void ToggleFlowerCam(bool lookingAtFlowers)
    {
        if(lookingAtFlowers)
            flowerCam.Priority = 10;
        else
            flowerCam.Priority = -10;
    }
}
