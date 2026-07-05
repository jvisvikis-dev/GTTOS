using System;
using UnityEngine;

public class Gnome : Interactable
{
    [SerializeField] private AudioClip gnomeSFX;
    public Action foundGnome;
    public override void Use()
    {
        AudioManager.Instance.Play3DSound(transform.position, gnomeSFX);
        foundGnome?.Invoke();
        Destroy(gameObject);
    }
}
