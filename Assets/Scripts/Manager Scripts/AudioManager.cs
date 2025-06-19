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
    public void PlayAudio(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Spawn the Audio source
        AudioSource audioSource = Instantiate(audioObject, spawnTransform.position, Quaternion.identity);
        // Assign Audioclip
        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLen = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLen);
    }
}
