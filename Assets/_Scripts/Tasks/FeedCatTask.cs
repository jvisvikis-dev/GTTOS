using System;
using UnityEngine;

public class FeedCatTask : Task
{
    [SerializeField] private Cat cat;

    private void Start()
    {
        cat.catFed += ClearTask;
    }

    private void ClearTask()
    {
        flags[0] = true;
    }
}
