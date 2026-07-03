using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouseTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private CinemachineCamera cam;
    private Interactable currentInteractable;
    private Controls _inputActions;

    private void Awake()
    {
        _inputActions = new Controls();
        _inputActions.Player.Use.performed += Use;
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 100, mask)){
            currentInteractable = hit.collider.gameObject.GetComponent<Interactable>();
            if (currentInteractable != null)
                currentInteractable.LookingAt();
        }
        else if (currentInteractable) 
        {
            StopLookingAtInteractable();
        }
        
    }

    private void StopLookingAtInteractable()
    {
        currentInteractable.EndLooking();
        currentInteractable = null;
    }

    private void Use(InputAction.CallbackContext context)
    {
        if(currentInteractable)
            currentInteractable.Use();
    }
}
