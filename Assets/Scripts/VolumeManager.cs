using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    //오디오 믹서
    public AudioMixer audioMixer;
    
    //bgm소리 조절 슬라이더
    public Slider bgmVolumeSlider;
    private float bgmSliderValue;

    //SFX 소리조절 슬라이더
    public Slider sfxVolumeSlider;
    private float sfxSliderValue;

    private void Start()
    {
        bgmVolumeSlider.value = bgmSliderValue;

        sfxVolumeSlider.value = sfxSliderValue;
    }

    public void SetBGMVolume()
    {
        if (bgmVolumeSlider.value > -20)
        {
            bgmSliderValue = bgmVolumeSlider.value;
        }
        else
        {
            bgmSliderValue = -80;
        }
        audioMixer.SetFloat("BgmVolume", bgmSliderValue);
    }

    public void SetSFXVolume()
    {
        if (sfxVolumeSlider.value > -20)
        {
            sfxSliderValue = sfxVolumeSlider.value;
        }
        else
        {
            sfxSliderValue = -80;
        }
        audioMixer.SetFloat("SfxVolume", sfxSliderValue);
    }
}
