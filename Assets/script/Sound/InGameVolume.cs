using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class InGameVolume : MonoBehaviour
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
        if (!PlayerPrefs.HasKey("BGMVolume"))
        {
            PlayerPrefs.SetFloat("BGMVolume", 1f);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1f);
            PlayerPrefs.Save();
        }
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        AM.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
        AM.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
        BGM.value = bgmVolume;
        SFX.value = sfxVolume;
    }

    private void Start()
    {
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        AM.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
        AM.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
        BGM.value = bgmVolume;
        SFX.value = sfxVolume;
    }
    public void SetBGMVolume(float volume)
    {
        AM.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(float volume)
    {
        AM.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}
