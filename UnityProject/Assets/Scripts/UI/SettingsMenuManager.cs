using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MenuManager
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;

    public static event Action<float> MusicVolumeChanged;
    public static event Action<float> SoundFXVolumeChanged;

    protected override void OnMenuManagerStart()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundFXVolumeSlider.value = PlayerPrefs.GetFloat("SoundFXVolume");

        musicVolumeSlider.onValueChanged.AddListener(v => MusicVolumeChanged?.Invoke(v));
        soundFXVolumeSlider.onValueChanged.AddListener(v => SoundFXVolumeChanged?.Invoke(v));
    }
}
