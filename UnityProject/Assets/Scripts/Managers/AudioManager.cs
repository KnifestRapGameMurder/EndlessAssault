using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundFXMixerGroup;
    [SerializeField] private Sound[] sounds;
    public Sound[] Sounds => sounds;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in Sounds)
        {
            sound.SetSource(gameObject.AddComponent<AudioSource>());
            sound.SetMixerGroup(sound.IsMusic ? musicMixerGroup : soundFXMixerGroup);
        }
    }

    public Sound GetSound(string name)
    {
        var sound = Array.Find<Sound>(Sounds, s => s.Name == name);
        if (sound == null) Debug.LogWarning($"Sound '{name}' was not found");
        return sound;
    }

    public void PlaySound(string name) => GetSound(name).Play();
    public void StopSound(string name) => GetSound(name).Stop();
}
