using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<Task> list = new();

    public bool allTasksDone()
    {
        foreach(Task task in list)
        {
            if (!task.isDone())
            {
                GiveAdviceOn(task);
                return false;
            }
        }
        return true;
    }   

    public void GiveAdviceOn(Task task)
    {

    }
    
}
