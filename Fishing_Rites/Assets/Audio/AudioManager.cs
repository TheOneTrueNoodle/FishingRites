using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    /*public void Start()
    {
       Play("Fish1");
    }*/

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound Error");


            return;
        }

        source.clip = s.clip;
        source.volume = s.volume;
        source.pitch = s.pitch;
        source.loop = s.loop;
        source.Play();
    }
}
