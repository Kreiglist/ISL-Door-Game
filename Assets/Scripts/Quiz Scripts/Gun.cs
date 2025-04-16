using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip walkSFX;

    public static Gun instance;
    private Animator gunAnimator;
    private SpriteRenderer sr;

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
        sr = GetComponent<SpriteRenderer>();
        gunAnimator = GetComponent<Animator>();
    }
    
    // PUBLIC FUNCTIONS
    public void GunAnimPlayer(string animName)
    {
        AnimationClip[] cutsceneClips = gunAnimator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in cutsceneClips)
        {
            switch (animName)
            {
                case "Shoot":
                    float shootLen = clip.length;
                    print("Got " + clip.name + " w/Length " + clip.length);
                    AudioManager.instance.PlayAudio(shootSFX, transform);
                    StartCoroutine(GunAnimPlayer(shootLen, "Shoot"));
                    
                    break;
                default:
                    print("Default is running");
                    break;
            }
        }
    }

    private IEnumerator GunAnimPlayer(float duration, string animName)
    {
        gunAnimator.SetBool(animName, true);
        yield return new WaitForSeconds(duration);
        gunAnimator.SetBool(animName, false);
        //sr.enabled = false;
    }
}
