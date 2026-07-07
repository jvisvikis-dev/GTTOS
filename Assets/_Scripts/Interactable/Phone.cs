using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Phone : Interactable
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform phonePlacement;
    [SerializeField] private AudioClip mumCall;
    [SerializeField] private AnimationCurve equipCurve;
    [SerializeField] private float timeToEquip = 0.5f;

    private Controls _inputActions;
    private Vector3 _origPos;
    private Quaternion _origRot;
    private bool inUse;
    private bool onPhone;
    public Action calledMum;

    private void Awake()
    {
        _inputActions = new Controls();
        _origPos = transform.position;
        _origRot = transform.rotation;
    }
    private void OnEnable()
    {
        _inputActions.Player.Drop.performed += Drop;
        _inputActions.Player.CallMum.performed += CallMum;
        _inputActions.Enable();
    }

    private void CallMum(InputAction.CallbackContext context)
    {
        if (mumCall && inUse && !onPhone)
        {
            onPhone = true;
            AudioManager.Instance.Play3DSound(transform.position, mumCall);
            bool isMum = true;
            StartCoroutine(WaitForCallToEnd(mumCall.length, isMum));
        }
    }

    private void OnDisable()
    {
        _inputActions.Player.Drop.performed -= Drop;
        _inputActions.Player.CallMum.performed -= CallMum;
        _inputActions.Disable();
    }

    private void Drop(InputAction.CallbackContext context)
    {
        if (!inUse)
            return;
        inUse = false;
        player.ToggleAllowedMovement();
        StartCoroutine(MovePhoneOverTime(_origPos, _origRot));
        UIManager.Instance.CloseControls();
    }

    public override void Use()
    {
        inUse = true;
        player.ToggleAllowedMovement();
        StartCoroutine(MovePhoneOverTime(phonePlacement.position, phonePlacement.rotation));
        UIManager.Instance.OpenControls("Call Mum");
    }

    public IEnumerator WaitForCallToEnd(float time, bool isMum)
    {
        yield return new WaitForSeconds(time);
        onPhone = false;
        if(isMum)
            calledMum?.Invoke();
    }

    public IEnumerator MovePhoneOverTime(Vector3 pos, Quaternion rot)
    {
        float timer = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        while(timer < timeToEquip)
        {
            transform.position = Vector3.Lerp(startPos,pos,equipCurve.Evaluate(timer/timeToEquip));
            transform.rotation = Quaternion.Lerp(startRot, rot, equipCurve.Evaluate(timer / timeToEquip));
            timer += Time.deltaTime;    
            yield return null;
        }
    }


}
