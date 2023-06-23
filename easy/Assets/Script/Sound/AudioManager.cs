using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    private AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        sfxSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
        PlayMusic();
    }
    public void PlayMusic()
    {
        if(musicSounds.Length > 0)
        {
            StartCoroutine(PlayOneMusic());
        }
    }

    IEnumerator PlayOneMusic()
    {
        int num = 0;
        while (num < musicSounds.Length)
        {
            musicSource.clip = musicSounds[num].clip;
            musicSource.Play();
            yield return new WaitForSeconds(musicSounds[num].clip.length);

            num++;
            if (num >= musicSounds.Length)
            {
                num = 0;
            }
        } 
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound Not found, check spelling");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
