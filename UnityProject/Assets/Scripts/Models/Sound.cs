using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [SerializeField] private AudioClip clip;
    public AudioClip Clip => clip;

    public string Name => Clip.name;

    [Range(0f, 1f)] [SerializeField] private float maxVolume;
    public float MaxVolume => maxVolume;

    private float _volume;
    public float Volume
    {
        get => _volume;
        set
        {
            _volume = Mathf.Clamp(value, 0f, MaxVolume);
            Source.volume = _volume;
        }
    }
    public void SetVolumeToMax() => Volume = MaxVolume;
    public void SetVolumeToHalf() => Volume = MaxVolume * 0.5f;

    [Range(-3f, 3f)] [SerializeField] private float pitch;
    public float Pitch => pitch;

    private float pitchAmplitude = 0.1f;

    private float _randPitch => Random.Range(Pitch - pitchAmplitude, Pitch + pitchAmplitude);

    [SerializeField] private bool isLooped;
    public bool IsLooped => isLooped;

    [SerializeField] private bool isMusic;
    public bool IsMusic => isMusic;

    private AudioSource _source;
    private AudioSource Source {
        get
        {
            if (_source == null) Debug.LogWarning($"Source of '{Name}' is null");
            return _source;
        }
        set
        {
            if (_source != null) Debug.LogWarning($"Replacing source of '{Name}'");
            _source = value;
        }
    }
    public void SetSource(AudioSource audioSource)
    {
        Source = audioSource;
        Source.clip = Clip;
        Source.name = Name;
        SetVolumeToMax();
        Source.volume = Volume;
        Source.pitch = Pitch;
        Source.loop = IsLooped;
    }

    public void SetMixerGroup(AudioMixerGroup audioMixerGroup)
    {
        Source.outputAudioMixerGroup = audioMixerGroup;
    }

    public void Play()
    {
        Source.pitch = _randPitch;
        Source.Play();
    }
    public void Stop() => Source.Stop();
}
