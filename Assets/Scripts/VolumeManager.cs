using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    //소리 조절 슬라이더
    public Slider volumeSlider;

    private AudioSource audioSource;

    private float sliderValue;

    private void Start()
    {
        audioSource = BGMManager.Instance.bgmSource;
        sliderValue = volumeSlider.value / 20;
        volumeSlider.value = sliderValue;
    }

    public void SetVolume()
    {
        sliderValue = volumeSlider.value / 20;
        audioSource.volume = sliderValue;
    }
}
