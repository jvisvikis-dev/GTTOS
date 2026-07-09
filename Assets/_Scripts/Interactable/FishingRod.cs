using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingRod : Interactable
{
    [SerializeField] private Vector3 pickUpOffset;
    [SerializeField] private Vector3 pickUpRotation;
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
    [SerializeField] private Fish fishPrefab;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private AudioClip castingSFX;
    [SerializeField] private AudioClip bobberHitWaterSFX;

    private Controls _inputActions;
    private bool bobberCasted;
    private bool isActive;
    private bool inHand;
    private bool fishReady;
    private bool bobberHitWater;
    private bool firstCatch;
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
                Fish fish = Instantiate(fishPrefab,bobber.transform);
                fish.transform.parent = null;
                fish.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
                if (!firstCatch)
                {
                    firstCatch = true;
                    fish.FirstCatch();
                    StartCoroutine(WaitForFirstCatchAnimation(fish));
                }
                else
                {
                    player.PickUp(fish,fish.FishOffset,fish.FishRotation);
                }
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
        player.itemDropped += RodDropped;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        bobber.hitWater -= BobberHitWater;
        player.itemDropped -= RodDropped;
    }
    public override void Use()
    {
        StartCoroutine(WaitForActive(0.5f));
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

    public void RodDropped()
    {
        inHand = false;
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

    public IEnumerator WaitForFirstCatchAnimation(Fish fish)
    {
        AudioManager.Instance.Play3DSound(fish.transform.position, fish.FirstCatchFanFare);
        AudioManager.Instance.StopBackgroundMusic();
        yield return new WaitForSeconds(fish.FirstCatchFanFare.length);
        AudioManager.Instance.PlayBackgroundMusic();
        fish.FinishAnimation();
        player.PickUp(fish, fish.FishOffset, fish.FishRotation);
    }

    public IEnumerator WaitForActive(float delay)
    {
        inHand = true;
        float timer = 0;
        while(timer < delay && inHand)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        if(inHand)
            isActive = true;
        else
            isActive = false;
    }
}
