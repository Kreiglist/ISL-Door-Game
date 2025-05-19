using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private AudioClip shootSFX;

    public static Gun instance;

    public float shootLen;
    private Animator gunAnimator;
    private SpriteRenderer gunSR;

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
        gunSR = GetComponent<SpriteRenderer>();
        gunAnimator = GetComponent<Animator>();
    }
    // PUBLIC FUNCTION
    public void GunAnimPlayer(string animName)
    {
        AnimationClip[] cutsceneClips = gunAnimator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in cutsceneClips)
        {
            switch (animName)
            {
                case "Shoot":
                    shootLen = clip.length;
                    AudioManager.instance.PlayAudio(shootSFX, transform);
                    StartCoroutine(GunAnimPlayer(shootLen, "Shoot"));
                    print("Got " + clip.name + " w/Length " + clip.length);
                    break;
                default:
                    gunSR.enabled = true;
                    break;
            }
        }
    }
    // IENUMERATOR
    private IEnumerator GunAnimPlayer(float duration, string animName)
    {
        gunAnimator.SetBool(animName, true);
        yield return new WaitForSeconds(duration);
        gunAnimator.SetBool(animName, false);
    }
}
