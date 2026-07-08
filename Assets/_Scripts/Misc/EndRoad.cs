using UnityEngine;

public class EndRoad : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private void OnTriggerEnter(Collider other)
    {
        player.ToggleAllowedMovement();
        UIManager.Instance.OpenEndGamePanel();
    }
}
