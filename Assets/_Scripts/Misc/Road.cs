using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] TaskManager taskManager;
    private void OnTriggerEnter(Collider other)
    {
        taskManager.allTasksDone();
    }
}
