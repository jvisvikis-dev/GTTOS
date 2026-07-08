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
    public Action stopForPlayer;

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
        UIManager.Instance.SetResultText("<color=green>You made it!");
        UIManager.Instance.SetAdviceText("Now you know how to safely cross the road! \nStay safe out there");
        Debug.Log("All tasks done!");
        stopForPlayer?.Invoke();
        return true;
    }   

    public void GiveAdviceOn(Task task)
    {
        UIManager.Instance.SetAdviceText(task.Advice);
    }

    public void KillPlayer()
    {
        UIManager.Instance.SetResultText("<color=red>You got run over!");
        killPlayer?.Invoke();
        Debug.Log("Failed Tasks");
    }

}
