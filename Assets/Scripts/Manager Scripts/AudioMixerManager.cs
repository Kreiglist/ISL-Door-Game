using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerManager : MonoBehaviour
{
    public static AudioMixerManager instance;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterValue", 1f);
        SetMasterVolume(masterSlider.value);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXValue", 1f);
        SetSFXVolume(sfxSlider.value);
    }
    public void SetMasterVolume(float level)
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("MasterValue", level);
    }
    public void SetSFXVolume(float level)
    {
        mixer.SetFloat("sfxVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("SFXValue", level);
    }
}