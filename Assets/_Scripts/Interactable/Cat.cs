using System;
using UnityEngine;
using UnityEngine.AI;

public class Cat : Interactable
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject[] runAwaySpots;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AudioClip meowSFX;
    [SerializeField] private AudioClip yippeeSFX;
    public Action catFed;
    private int spotIdx = 0;
    private bool running = false;
    public override void Use()
    {
        if(player && player.HasFish())
        {
            animator.SetTrigger("backflip");
            player.ClearItemInHand();
            AudioManager.Instance.Play3DSound(transform.position, yippeeSFX);
            catFed?.Invoke();
        }
        else
        {
            RunAway();
        }
        
    }

    private void Update()
    {
        if (player && player.HasFish())
            InteractableUIText = "Give";
        else
            InteractableUIText = "Pet";

        if (agent.remainingDistance <= 0.01f)
        {
            running = false;
            animator.SetBool("running", running);
            agent.destination = agent.transform.position;
        }
        var n = Vector3.zero;
        if (!running && player)
            n = player.transform.position - transform.position;
        else
            n = agent.destination - transform.position;
        Quaternion endRotation = Quaternion.LookRotation(n);
        endRotation.x = 0f;
        endRotation.z = 0f;
        transform.rotation = endRotation;
    }

    private void RunAway()
    {
        Vector3 spot = runAwaySpots[++spotIdx % runAwaySpots.Length].transform.position;
        agent.SetDestination(spot);
        running = true;
        animator.SetBool("running", running);
        AudioManager.Instance.Play3DSound(transform.position, meowSFX);
    }

}
