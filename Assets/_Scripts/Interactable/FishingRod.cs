using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingRod : Interactable
{
    [SerializeField] private Vector3 pickUpOffset;
    [SerializeField] private Vector3 pickUpRotation;
    [SerializeField] private Vector3 fishOffset;
    [SerializeField] private Vector3 fishRotation;
    [SerializeField] private float castUpwardsForce = 2f;
    [SerializeField] private float castForce = 5f;
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 5f;
    [Header("References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator bobberAnimator;
    [SerializeField] private Animator rodAnimator;
    [SerializeField] private Bobber bobber;
    [SerializeField] private Rigidbody bobberRB;
    [SerializeField] private GameObject bobberHome;
    [SerializeField] private ParticleSystem splashParticles;
    [SerializeField] private Interactable fishPrefab;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private AudioClip castingSFX;
    [SerializeField] private AudioClip bobberHitWaterSFX;

    private Controls _inputActions;
    private bool bobberCasted;
    private bool isActive;
    private bool fishReady;
    private bool bobberHitWater;
    private void Awake()
    {
        _inputActions = new Controls();
        _inputActions.Player.Use.performed += UseBobber;
    }

    private void UseBobber(InputAction.CallbackContext context)
    {
        if (!isActive)
            return;

        if (bobberRB && !bobberCasted)
        {
            bobberCasted = true;
            bobberRB.isKinematic = false;
            bobberRB.AddForce((player.transform.forward).normalized * castForce + player.transform.up*castUpwardsForce, ForceMode.Impulse);
            rodAnimator.SetBool("isCasted", bobberCasted);
            AudioManager.Instance.Play3DSound(transform.position, castingSFX);
        }
        else
        {
            if (fishReady)
            {
                fishReady = false;
                Interactable fish = Instantiate(fishPrefab,bobber.transform);
                fish.transform.parent = null;
                player.PickUp(fish,fishOffset,fishRotation);
                isActive = false;
            }
            bobberHitWater = false;
            bobberCasted = false;
            bobberRB.isKinematic = true;
            bobberAnimator.SetBool("Fishing", bobberCasted);
            splashParticles.Stop();
            bobber.transform.parent = bobberHome.transform;
            bobber.transform.localPosition = Vector3.zero;
            bobber.transform.localRotation = Quaternion.identity;
            rodAnimator.SetBool("isCasted", bobberCasted);
        }
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, bobberHome.transform.position);
        lineRenderer.SetPosition(1, bobber.Mesh.transform.position);
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        bobber.hitWater += BobberHitWater;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        bobber.hitWater -= BobberHitWater;
    }
    public override void Use()
    {
        isActive = true;
        if (player)
            player.PickUp(this, pickUpOffset, pickUpRotation);
    }

    public void BobberHitWater(Vector3 hitPos)
    {
        if (bobberHitWater || !isActive)
            return;
        bobberHitWater = true;
        bobberAnimator.SetBool("Fishing", true);
        bobberRB.position = hitPos;
        bobberRB.isKinematic = true;
        bobber.transform.parent = null;
        AudioManager.Instance.Play3DSound(bobber.transform.position,bobberHitWaterSFX);
        StartCoroutine(WaitForFish());
    }

    public void ReadyToCatch()
    {
        splashParticles.Play();
        fishReady = true;
    }

    public IEnumerator WaitForFish()
    {
        float waitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
        float timer = 0;
        while (timer < waitTime && bobberCasted)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        if(bobberCasted)
            ReadyToCatch();
    }
}
