using UnityEngine;

public class CallMumTask : Task
{
    [SerializeField] private Phone phone;

    private void OnEnable()
    {
        phone.calledMum += MumCalled;
    }

    private void OnDisable()
    {
        phone.calledMum -= MumCalled;
    }
    public void MumCalled()
    {
        flags[0] = true;
    }
}
