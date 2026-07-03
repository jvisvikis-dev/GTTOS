using UnityEngine;

public class EndRoad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.SetResultText("You made it to the other side!");
        UIManager.Instance.SetAdviceText("Now you know how to safely cross the road! Stay stafe out there");
        UIManager.Instance.OpenEndGamePanel();
    }
}
