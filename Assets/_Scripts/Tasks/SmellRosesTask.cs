using System;
using UnityEngine;

public class SmellRosesTask : Task
{
    [SerializeField] private Flowers flowers;

    private void Start()
    {
        flowers.smelledRoses += clearedTask;
    }

    private void clearedTask()
    {
        flags[0] = true;
    }
}
