using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [SerializeField] private AudioMixer _audioMixer;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SettingsMenuManager.MusicVolumeChanged += OnMusicVolumeChanged;
        SettingsMenuManager.SoundFXVolumeChanged += OnSoundFXVolumeChanged;

        _audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        _audioMixer.SetFloat("SoundFXVolume", PlayerPrefs.GetFloat("SoundFXVolume"));
    }

    private void OnMusicVolumeChanged(float volume)
    {
        _audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    private void OnSoundFXVolumeChanged(float volume)
    {
        _audioMixer.SetFloat("SoundFXVolume", volume);
        PlayerPrefs.SetFloat("SoundFXVolume", volume);
    }
}
