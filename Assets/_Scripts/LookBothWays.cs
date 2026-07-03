using UnityEngine;

public class LookBothWays : Task
{
    [SerializeField] private int minLeftCount;
    [SerializeField] private int minRightCount;
    private void Awake()
    {
        flags = new bool[2];
    }

    public void LookedLeft(int lookCount)
    {
        if (lookCount >= minLeftCount)
        {
            flags[0] = true;
        }
    }

    public void LookedRight(int lookCount)
    {
        if (lookCount >= minRightCount)
        {
            flags[1] = true;
        }
    }

}
