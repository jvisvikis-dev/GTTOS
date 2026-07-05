using System;
using UnityEngine;

public class GnomeTask : Task
{
    [SerializeField] private Gnome gnome;

    private void Start()
    {
        gnome.foundGnome += clearTask;
    }

    private void clearTask()
    {
        flags[0] = true;
    }
}
