using UnityEngine;

public class Rubbish : Interactable
{
    private PlayerController _player;

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerController>();
    }
    public override void Use()
    {
        _player.PickUp(this);
    }
}
