using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = 1;
            s.source.loop = s.loop;
        }
    }

    /*static IEnumerator FadeIn(Sound s, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = s.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            s.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    private void FadeInCaller(Sound s, float duration, float targetVolume)
    {
        StartCoroutine(FadeIn(s, duration, targetVolume));
    }*/

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s.loop == false)
        {
            s.source.volume = 2;
        }
        s.source.Play();
        /*if(s.fade == true)
        {
            FadeInCaller(s, 1, 3);
        }*/

        if (s.source == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
