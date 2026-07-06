using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingRod : Interactable
{
    [SerializeField] private Vector3 pickUpOffset;
    [SerializeField] private Vector3 pickUpRotation;
    [SerializeField] private float castForce = 5f;
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 5f;
    [Header("References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator animator;
    [SerializeField] private Bobber bobber;
    [SerializeField] private Rigidbody bobberRB;
    [SerializeField] private GameObject bobberHome;
    [SerializeField] private ParticleSystem splashParticles;
    [SerializeField] private Interactable fishPrefab;

    private Controls _inputActions;
    private bool bobberCasted;
    private bool isActive;
    private bool fishReady;
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
            bobberRB.AddForce((player.transform.forward - (player.transform.right/4)).normalized * castForce, ForceMode.Impulse);
        }
        else
        {
            animator.SetBool("Fishing", false);
            splashParticles.Stop();
            bobberCasted = false;
            bobberRB.isKinematic = true;
            bobber.transform.parent = bobberHome.transform;
            bobberRB.transform.position = bobberHome.transform.position;
            if (fishReady)
            {
                fishReady = false;
                Interactable fish = Instantiate(fishPrefab,bobber.transform);
                fish.transform.parent = null;
                player.PickUp(fish);
            }
        }
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
        animator.SetBool("Fishing", true);
        bobberRB.position = hitPos;
        bobberRB.isKinematic = true;
        bobber.transform.parent = null;
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
        ReadyToCatch();
    }
}
