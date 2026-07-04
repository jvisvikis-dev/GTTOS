using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<Task> list = new();
    [SerializeField] private PlayerController player;
    public Action killPlayer;

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
        UIManager.Instance.SetResultText("You made it to the other side!");
        Debug.Log("All tasks done!");
        return true;
    }   

    public void GiveAdviceOn(Task task)
    {
        UIManager.Instance.SetAdviceText(task.Advice);
    }

    public void KillPlayer()
    {
        UIManager.Instance.SetResultText("You got run over!");
        killPlayer?.Invoke();
        Debug.Log("Failed Tasks");
    }

}
