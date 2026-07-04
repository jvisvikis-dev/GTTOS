using System;
using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] protected string taskID;
    [SerializeField] protected string advice;
    [SerializeField] protected bool[] flags;

    public string Advice => advice;

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
