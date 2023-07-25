using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeUI : MonoBehaviour
{
    public Slider sfxVolSlider, musicVolSlider;

    private void Start()
    {
        sfxVolSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
        musicVolSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
    }
    public void ChangeSFXVolume()
    {
        AudioManager.Instance.SfxVolumeChanged(sfxVolSlider.value);
    }
    public void ChangeMusicVolume()
    {
        AudioManager.Instance.MusicVolumeChanged(musicVolSlider.value);
    }
}
