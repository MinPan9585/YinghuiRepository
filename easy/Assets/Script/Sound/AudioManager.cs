using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    private AudioSource musicSource, sfxSource;

    public Dictionary<int, AudioSource> sfxSources = new Dictionary<int, AudioSource>();

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
    public int PlaySFXLoop(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound Not found, check spelling");
            return -1;
        }
        else
        {
            AudioSource sfx = PoolManager.Instance.
                SpawnFromSubPool(sfxSource.name.ToString(), transform).
                GetComponent<AudioSource>();
            
            sfx.clip = s.clip;
            sfx.loop = true;
            sfx.Play();

            int id = PoolManager.Instance.GetPooledIndex(sfx.gameObject);
            
            if (!sfxSources.ContainsKey(id))
            { sfxSources.Add(id, sfx); }

            return id;
        }
    }
    public void StopSFXLoop(int sfxID)
    {
        if(sfxID < 0 || sfxID > sfxSources.Count)
        {
            Debug.LogWarning("Sound Not found, and can't be retrieved");
        }
        else
        {
            PoolManager.Instance.DespawnFromSubPool(sfxSources[sfxID].gameObject,sfxID);
        }
    }
    public void SfxVolumeChanged(float value)
    {
        sfxSource.volume = value;
    }
    public void MusicVolumeChanged(float value)
    {
        musicSource.volume = value;
    }
}
