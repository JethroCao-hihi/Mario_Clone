using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public void Start()
    {
        LoadVolume();
        
        // Add listeners to sliders
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        }
        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(UpdateSoundVolume);
        }
    }
    
    public void Play()
    {
        SceneManager.LoadScene("Map 1");
    }
    
    public void Quit()
    {
        // Thoát game
        Application.Quit();
        
        // Dành cho Unity Editor (?? test trong Editor)
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    public void UpdateMusicVolume(float volume)
    {
        // Update AudioManager if it exists
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(volume);
        }
        
        // Save immediately
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    
    public void UpdateSoundVolume(float volume)
    {
        // Update AudioManager if it exists
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(volume);
        }
        
        // Save immediately
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    
    public void SaveVolume()
    {
        // Save slider values
        if (musicSlider != null)
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        if (sfxSlider != null)
            PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        
        PlayerPrefs.Save();
    }
    
    public void LoadVolume()
    {
        // Load saved volume values (default to 0.7 for music, 1.0 for SFX)
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        
        // Set slider values without triggering onValueChanged
        if (musicSlider != null)
        {
            musicSlider.SetValueWithoutNotify(savedMusicVolume);
        }
        if (sfxSlider != null)
        {
            sfxSlider.SetValueWithoutNotify(savedSFXVolume);
        }
        
        // Apply to AudioManager if it exists
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(savedMusicVolume);
            AudioManager.Instance.SetSFXVolume(savedSFXVolume);
        }
    }
}
