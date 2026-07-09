using UnityEngine;

public class Fish : Interactable
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioClip firstCatchFanFare;
    [SerializeField] private Vector3 fishOffset;
    [SerializeField] private Vector3 fishRotation;
    public Vector3 FishOffset => fishOffset;
    public Vector3 FishRotation => fishRotation;
    public AudioClip FirstCatchFanFare => firstCatchFanFare;
    private PlayerController _player;

    private void Awake()
    {
        _player = FindFirstObjectByType<PlayerController>();
    }
    public override void Use() 
    {
        _player.PickUp(this, fishOffset, fishRotation);
    }

    public void FirstCatch()
    {
        rb.isKinematic = true;
        animator.SetTrigger("Caught");
    }

    public void FinishAnimation()
    {
        rb.isKinematic = false;
        animator.SetTrigger("Finish");
    }

}
