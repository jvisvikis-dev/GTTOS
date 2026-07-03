using Unity.Cinemachine;
using UnityEngine;

public class PlayerMouseTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private CinemachineCamera cam;
    private Interactable currentInteractable;

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 100, mask)){
            if (currentInteractable != null)
            {
                StopLookingAtInteractable();
            }
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
}
