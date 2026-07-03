using UnityEngine;

public class LookSide : Interactable
{
    [SerializeField] private LookBothWays task;
    [SerializeField] private bool isRight = false;
    private int lookCount = 0;
    private bool lookedAt = false;

    public void LookedAt()
    {
        lookedAt = true;
        lookCount++;
        Debug.Log($"Looked at side {lookCount} times");
        if(isRight)
            task.LookedRight();
        else
            task.LookedLeft();
    }
    public override void LookingAt()
    {
        if (!lookedAt)
            LookedAt();
    }

    public override void EndLooking()
    {
        lookedAt = false;
    }

    public override void Use()
    {
        
    }
}
