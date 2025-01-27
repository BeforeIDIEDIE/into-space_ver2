using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option_Volume : MonoBehaviour
{
    [SerializeField] private AudioMixer AM;
    [SerializeField] private Slider SFX;
    [SerializeField] private Slider BGM;

    private void Awake()
    {
        //m_MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        //m_MusicBGMSlider.onValueChanged.AddListener(SetMusicVolume);
        //m_MusicSFXSlider.onValueChanged.AddListener(SetSFXVolume);
        SFX.onValueChanged.AddListener(SetSFXVolume);
        BGM.onValueChanged.AddListener(SetBGMVolume);
    }
    public void SetBGMVolume(float volume)
    {
        AM.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume(float volume)
    {
        AM.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

}
