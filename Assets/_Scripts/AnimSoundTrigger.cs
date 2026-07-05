using UnityEngine;

public class AnimSoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlayAudio()
    {
        audioSource.Play();
    }
}
