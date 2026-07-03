using UnityEngine;

public class LookBothWays : Task
{
    private void Awake()
    {
        taskID = "LookBothWays";
        flags = new bool[2];
    }

    public void LookedLeft()
    {
        Debug.Log("LookedLeft");
        flags[0] = true;
    }

    public void LookedRight()
    {
        Debug.Log("LookedRight");
        flags[1] = true;
    }

}
