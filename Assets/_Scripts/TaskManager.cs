using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<Task> list = new();
    [SerializeField] private PlayerController player;

    public bool allTasksDone()
    {
        foreach(Task task in list)
        {
            if (!task.isDone())
            {
                KillPlayer();
                GiveAdviceOn(task);
                return false;
            }
        }
        Debug.Log("All tasks done!");
        return true;
    }   

    public void GiveAdviceOn(Task task)
    {

    }

    public void KillPlayer()
    {
        Destroy(player.gameObject);
        Debug.Log("Failed Tasks");
    }

}
