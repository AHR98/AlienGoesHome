using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject GamePanel;
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public GameObject MainMenuPanel;
    public GameObject GameOverPanel;

    public AudioMixer audioMixer;
    public Slider SFXSlider;
    public Slider musicSlider;
    private void ClearPanels()
    {
        PausePanel.SetActive(false);
        GamePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    public void ShowPause()
    {
        ClearPanels();
        PausePanel.SetActive(true);
    }

    public void ShowGamePanel()
    {
        ClearPanels();
        GamePanel.SetActive(true);
    }

    public void ShowSettingsPanel()
    {
        ClearPanels();
        SettingsPanel.SetActive(true);
    }

    public void ShowMainMenu()
    {
        ClearPanels();
        MainMenuPanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        ClearPanels();
        GameOverPanel.SetActive(true);
    }
    public void ApplyAudioMixerSettings()
    {
        float masterVolume = 0f;

       
        if (audioMixer.GetFloat("MusicVolume", out masterVolume))
        {
            musicSlider.value = masterVolume;
        }
        if (audioMixer.GetFloat("SFXVolume", out masterVolume))
        {
            SFXSlider.value = masterVolume;
        }
    }


    public void OnGlobalMusicChange(float globalMusic)
    {
        audioMixer.SetFloat("MusicVolume", globalMusic);

    }

    public void OnGlobalSoundFXChange(float globalSoundEffects)
    {
        audioMixer.SetFloat("SFXVolume", globalSoundEffects);

    }
}
