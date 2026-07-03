using System;
using UnityEngine;

public class Task : MonoBehaviour
{
    public string taskID;
    public string advice;
    public bool[] flags;

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
