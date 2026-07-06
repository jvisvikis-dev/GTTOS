using UnityEngine;
using UnityEngine.AI;

public class Cat : Interactable
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject[] runAwaySpots;
    [SerializeField] private NavMeshAgent agent;
    private int spotIdx = 0;
    private bool running = false;
    public override void Use()
    {
        if(player && player.HasFish())
        {
            animator.SetTrigger("backflip");
        }
        else
        {
            RunAway();
        }
        
    }

    private void Update()
    {
        if (agent.remainingDistance <= 0.01f)
        {
            running = false;
            animator.SetBool("running", running);
            agent.destination = agent.transform.position;
        }
        var n = Vector3.zero;
        if (!running)
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
    }

}
