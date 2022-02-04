using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundFXSource;


    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundFXSlider;
    public GameObject pausePanel;


    // Start is called before the first frame update
    void Start()
    {
        musicSource = Music._instance.GetComponent<AudioSource>();
        soundFXSource = SoundFx._instance.GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            LoadMusicVolume();

        }
        else
        {
            LoadMusicVolume();

        }
        if (!PlayerPrefs.HasKey("SoundFXVolume"))
        {
            PlayerPrefs.SetFloat("SoundFXVolume", 0.5f);
            LoadSoundFXVolume();

        }
        else
        {
            LoadSoundFXVolume();

        }
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        soundFXSource.volume = PlayerPrefs.GetFloat("SoundFXVolume");
        LoadMusicVolume();
        LoadSoundFXVolume();
    }


    public void clickSound()
    {
        soundFXSource.Play();
    }


    public void ChangeMusicVolume()
    {
        musicSource.volume = musicSlider.value;
        SaveMusicVolume();
    }
    public void ChangeSoundFXVolume()
    {
        soundFXSource.volume = soundFXSlider.value;
        SaveSoundFXVolume();
    }

    public void LoadMusicVolume() 
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void LoadSoundFXVolume()
    {
        soundFXSlider.value = PlayerPrefs.GetFloat("SoundFXVolume");
    }

    public void SaveMusicVolume() 
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SaveSoundFXVolume()
    {
        PlayerPrefs.SetFloat("SoundFXVolume", soundFXSlider.value);
    }

    public void ActivatePanel()
    {
        pausePanel.SetActive(true);
        LoadMusicVolume();
        LoadSoundFXVolume();
        Time.timeScale = 0f;
    }

    public void DeActivatePanel()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
