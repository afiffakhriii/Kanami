using UnityEngine;

public class BacksoundManager : MonoBehaviour
{
    public AudioClip backsoundClip; // Assign your backsound audio clip in the Inspector
    private AudioSource audioSource;

    // Singleton instance
    private static BacksoundManager instance;
    public static BacksoundManager Instance { get { return instance; } }

    void Awake()
    {
        // Ensure only one instance of BacksoundManager exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = backsoundClip;
        audioSource.loop = true;

        // Ensure audio follows system volume controls
        audioSource.outputAudioMixerGroup = null; // Ensure the AudioSource uses the default audio output
    }

    void Start()
    {
        PlayBacksound();
    }

    // Function to play the backsound
    public void PlayBacksound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Function to stop the backsound
    public void StopBacksound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Function to set the volume
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
