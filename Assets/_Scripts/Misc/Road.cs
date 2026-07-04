using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;
    private bool triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            triggered = true;
            taskManager.allTasksDone();
        }
    }
}
