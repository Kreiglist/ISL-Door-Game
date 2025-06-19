using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public void SetMasterVolume(float level)
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
    }
    public void SetSFXVolume(float level)
    {
        mixer.SetFloat("sfxVolume", Mathf.Log10(level) * 20f);
    }
}
