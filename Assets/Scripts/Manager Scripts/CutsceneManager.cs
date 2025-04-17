using System.Collections;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;

    //[SerializeField] private GameObject menuCutscene;
    [SerializeField] private GameObject cutsceneScreen;
    [SerializeField] private AudioClip walkSFX;

    public float walkLen;
    private Animator cutsceneScreenAnimator;
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
        cutsceneScreenAnimator = cutsceneScreen.GetComponent<Animator>();
    }
    public void CutscenePlayer(string animName)
    {
        AnimationClip[] cutsceneClips = cutsceneScreenAnimator.runtimeAnimatorController.animationClips;

        foreach(AnimationClip clip in cutsceneClips)
        {
            switch(animName)
            {
                case "Walk":
                    cutsceneScreen.SetActive(true);
                    walkLen = clip.length;
                    AudioManager.instance.PlayAudio(walkSFX, transform);
                    StartCoroutine(PlayCutscene(walkLen, "Walk"));
                    break;
                default:
                    cutsceneScreen.SetActive(false);
                    break;
            }
        } 
    }
    private IEnumerator PlayCutscene(float duration, string animName)
    {
        cutsceneScreenAnimator.Play(animName);
        yield return new WaitForSeconds(duration);
    }
}
