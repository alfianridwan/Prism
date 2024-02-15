using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;

    public Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if (s.isMusic)
                s.source.outputAudioMixerGroup = musicMixerGroup;
            else
                s.source.outputAudioMixerGroup = sfxMixerGroup;
        }
    }

    public void Play(string soundName, bool randomisePitch = false)
    {
        Sound s = GetSound(soundName);
        if (s == null)
        {
            return;
        }

        s.source.Play();

        if (randomisePitch)
            s.source.pitch = Random.Range(s.pitch - .1f, s.pitch + .3f);
    }

    public void PlayLoop(string soundName)
    {
        Sound s = GetSound(soundName);
        if (s == null)
        {
            return;
        }


        s.source.loop = true;
        s.source.Play();
    }

    public void StopLoop(string soundName)
    {
        Sound s = GetSound(soundName);
        if (s == null)
        {
            return;
        }


        s.source.loop = false;
        s.source.Stop();
    }

    public void PlayPitch(string soundName, float pitch)
    {
        Sound s = GetSound(soundName);
        if (s == null)
        {
            return;
        }

        s.source.pitch = pitch;
        s.source.Play();
    }


    public void PlayIndex(int index)
    {
        if (index < 0 || index >= sounds.Length)
        {
            return;
        }

        Sound s = sounds[index];

        s.source.Play();
    }

    public void Stop(string soundName)
    {
        Sound s = GetSound(soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void UpdateMusicVolume(float volume)
    {
        musicMixerGroup.audioMixer.SetFloat("musicVolume", Mathf.Log(volume) * 20);
        AudioController.instance.Play("Button Click");
    }

    public void UpdateSFXVolume(float volume)
    {
        sfxMixerGroup.audioMixer.SetFloat("sfxVolume", Mathf.Log(volume) * 20);
        AudioController.instance.Play("Button Click");
    }

    Sound GetSound(string soundName)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == soundName)
                return s;
        }

        return null;
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    public bool loop = false;

    public bool isMusic = false;

    [HideInInspector]
    public AudioSource source;
}
