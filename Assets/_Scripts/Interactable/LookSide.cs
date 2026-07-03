using UnityEngine;

public class LookSide : Interactable
{
    [SerializeField] private LookBothWays [] tasks;
    [SerializeField] private bool isRight = false;
    [SerializeField] private Renderer material;
    private int lookCount = 0;
    private bool lookedAt = false;

    public void LookedAt()
    {
        lookedAt = true;
        material.material.color = Color.green;
        lookCount++;
        foreach (LookBothWays task in tasks)
        {
            if (isRight)
                task.LookedRight(lookCount);
            else
                task.LookedLeft(lookCount);
        }
    }
    public override void LookingAt()
    {
        if (!lookedAt)
            LookedAt();
    }

    public override void EndLooking()
    {
        material.material.color = Color.red;
        lookedAt = false;
    }

    public override void Use() { }
}
