using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private AudioClip shootSFX;

    public static Gun instance;

    public float shootLen;
    private Animator gunAnimator;
    private SpriteRenderer gunSR;
    private Camera mainCamera;

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

        mainCamera = Camera.main;
    }
    void Update()
    {
        Vector3 screenPos = Input.mousePosition;
        // Convert screen position to world point
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, mainCamera.nearClipPlane + 10f));

        // Set gun's position — keep z consistent if needed
        transform.position = new Vector3(worldPos.x, -1.5f, transform.position.z);
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
                    AudioManager.instance.PlayAudio(shootSFX, transform, 1f);
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
