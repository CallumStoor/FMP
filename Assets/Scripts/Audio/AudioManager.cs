using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if(s.Background)
            {
                Play(s.name);
            }
        }

        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not Found!");
            return;
        }
        s.source.Play();
    }
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    public string name;

    [Range(0,1)]
    public float volume;
    [Range(0.1f, 3)]
    public float pitch;

    public bool loop;

    public bool Background;

    [HideInInspector]
    public AudioSource source;
}
