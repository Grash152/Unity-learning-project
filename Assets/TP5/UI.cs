using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private AudioMixer TP5;
    [SerializeField] private AudioMixerSnapshot[] snapshots;
    [SerializeField] private Slider MasterBar;
    [SerializeField] private Slider VoiceBar;
    [SerializeField] private Slider MusicBar;
    [SerializeField] private Slider EnvBar;
    [SerializeField] private Slider SFXBar;
    [SerializeField] private TMP_Dropdown dropdown;
   


    private void Start()
    {
        snapshots[0].TransitionTo(1f);
    }

    // Update is called once per frame
    void Update()
    {
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
        MasterBar.onValueChanged.AddListener(SetMasterVol);
        SFXBar.onValueChanged.AddListener(SetSFXVol);
        VoiceBar.onValueChanged.AddListener(SetVoiceVol);
        MusicBar.onValueChanged.AddListener(SetMusicVol);
        EnvBar.onValueChanged.AddListener(SetEnvVol);
    }

    void OnDropdownChanged(int index)
    {
     snapshots[index].TransitionTo(1f);
    }

    void SetMasterVol(float volume)
    {
        TP5.SetFloat("Volume", volume);
    }
    void SetSFXVol(float volume)
    {
        TP5.SetFloat("SFXVolume", volume);
    }
    void SetVoiceVol(float volume)
    {
        TP5.SetFloat("VoiceVolume", volume);
    }
    void SetMusicVol(float volume)
    {
        TP5.SetFloat("MusicVolume", volume);
    }
    void SetEnvVol(float volume)
    {
        TP5.SetFloat("EnvVolume", volume);
    }
}
