using UnityEngine;

public class BinTask : Task
{
    [SerializeField] private Bin bin;

    private void Start()
    {
        bin.trashCleared += taskCleared;
    }

    private void taskCleared()
    {
        flags[0] = true;
    }
}
