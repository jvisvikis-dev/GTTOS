using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;
    [SerializeField] private AudioSource audioSourcePrefab;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField][Range(0f, 1f)] private float backgroundMusicVolume;
    [SerializeField][Range(0f, 1f)] private float volume;

    private AudioSource backgroundMusicObject;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        Play2DSound(backgroundMusic, true);
    }

    public void Play3DSound(Vector3 position, AudioClip clip)
    {
        AudioSource audioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1;
        audioSource.Play();
        Destroy(audioSource.gameObject, clip.length);
    }

    public void Play2DSound(AudioClip clip, bool isBackground = false)
    {
        AudioSource audioSource = Instantiate(audioSourcePrefab);
        audioSource.spatialBlend = 0;
        audioSource.clip = clip;
        audioSource.volume = isBackground ? backgroundMusicVolume : volume;
        audioSource.loop = isBackground;
        audioSource.Play();
        if (isBackground)
            backgroundMusicObject = audioSource;
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicObject.Stop();
    }
}
