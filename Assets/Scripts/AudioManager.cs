using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource sfxSource; 
    public AudioSource musicSource;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Create audio sources
            sfxSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true; // Music should loop
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Play a sound effect.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// Play background music.
    /// </summary>
    /// <param name="clip">The audio clip to play as music.</param>
    public void PlayMusic(AudioClip clip)
    {
        if (clip != null && musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    /// <summary>
    /// Stop background music.
    /// </summary>
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }
    /// <summary>
    /// Set the volume for sound effects.
    /// </summary>
    /// <param name="volume">Volume value (0 to 1).</param>
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// Set the volume for music.
    /// </summary>
    /// <param name="volume">Volume value (0 to 1).</param>
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }
}
