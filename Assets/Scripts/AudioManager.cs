using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("Volume Settings")]
    [SerializeField] [Range(0f, 1f)] private float musicVolume = 0.7f;
    [SerializeField] [Range(0f, 1f)] private float sfxVolume = 1f;

    [Header("Background Music")]
    public AudioClip backgroundMusic;
    public AudioClip winMusic;
    public AudioClip gameOverMusic;

    [Header("Sound Effects")]
    public AudioClip dead;
    public AudioClip collectCoin;
    public AudioClip jump;
    public AudioClip enemyDeath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Load saved volume settings
        LoadVolumeSettings();
    }

    private void Start()
    {
        ApplyVolumeSettings();
        PlayBackgroundMusic();
    }

    private void LoadVolumeSettings()
    {
        // Load saved volumes from PlayerPrefs (default to 0.7 for music, 1.0 for SFX)
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    }

    private void ApplyVolumeSettings()
    {
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
        if (SFXSource != null)
        {
            SFXSource.volume = sfxVolume;
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
        // Save to PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (SFXSource != null)
        {
            SFXSource.volume = sfxVolume;
        }
        // Save to PlayerPrefs
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayWinMusic()
    {
        if (musicSource != null && winMusic != null)
        {
            musicSource.Stop();
            musicSource.clip = winMusic;
            musicSource.loop = false;
            musicSource.Play();
        }
    }

    public void PlayGameOverMusic()
    {
        if (musicSource != null && gameOverMusic != null)
        {
            musicSource.Stop();
            musicSource.clip = gameOverMusic;
            musicSource.loop = false;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource != null && clip != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void PlayMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.Play();
        }
    }
}

