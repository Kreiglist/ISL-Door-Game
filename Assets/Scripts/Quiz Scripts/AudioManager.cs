using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource audioObject;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    public void PlayAudio(AudioClip audioClip, Transform spawnTransform)
    {
        // Spawn GameObject
        AudioSource audioSource = Instantiate(audioObject, spawnTransform.position, Quaternion.identity);
        // Assign Audioclip
        audioSource.clip = audioClip;

        audioSource.Play();

        float clipLen = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLen);
    }
}
