using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Environment : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Start()
    {
        masterVolumeSlider.onValueChanged.AddListener(value => SoundeManager.Instance.SetMasterVolume(value));
        bgmVolumeSlider.onValueChanged.AddListener(value => SoundeManager.Instance.SetBGMVolume(value));
        sfxVolumeSlider.onValueChanged.AddListener(value => SoundeManager.Instance.SetSFXVolume(value));
    }
}
