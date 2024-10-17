using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    //�Ҹ� ���� �����̴�
    public Slider volumeSlider;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = BGMManager.Instance.bgmSource;
    }

    public void SetVolume()
    {
        audioSource.volume = volumeSlider.value / 20;
    }
}
