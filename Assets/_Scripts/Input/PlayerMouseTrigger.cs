using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouseTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private int interactableRange;
    private Interactable currentInteractable;
    private Controls _inputActions;

    private void Awake()
    {
        _inputActions = new Controls();
        
    }
    private void OnEnable()
    {
        _inputActions.Player.Use.performed += Use;
        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Player.Use.performed -= Use;
        _inputActions.Disable();
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, interactableRange, mask)){
            Interactable newInteractable = hit.collider.gameObject.GetComponent<Interactable>();
            if (currentInteractable != null && newInteractable != currentInteractable)
            {
                currentInteractable.EndLooking();
            }
            currentInteractable = newInteractable;
            if (currentInteractable != null)
                currentInteractable.LookingAt();
        }
        else if (currentInteractable) 
        {
            StopLookingAtInteractable();
        }
        else
        {
            UIManager.Instance.ClearInteractableText();
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
