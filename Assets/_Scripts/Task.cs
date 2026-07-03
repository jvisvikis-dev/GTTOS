using System;
using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] protected string taskID;
    [SerializeField] protected string advice;
    protected bool[] flags;

    public bool isDone()
    {
        foreach(bool flag in flags)
        {
            if (!flag)
            {
                return false;
            }
        }
        return true;
    }
}
