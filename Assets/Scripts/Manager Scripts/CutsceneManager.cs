using System.Collections;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;

    //[SerializeField] private GameObject menuCutscene;
    [SerializeField] private GameObject cutsceneScreen;
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
    public void CutscenePlayer(string anim)
    {
        AnimationClip[] cutsceneClips = cutsceneScreenAnimator.runtimeAnimatorController.animationClips;

        foreach(AnimationClip clip in cutsceneClips)
        {
            switch(anim)
            {
                case "Walk":
                    cutsceneScreen.SetActive(true);
                    float walkLen = clip.length;
                    print("Got " + clip.name + " w/Length " + clip.length);
                    StartCoroutine(PlayCutscene(walkLen, "Walk"));
                    break;
                case "Test":
                    cutsceneScreen.SetActive(true);
                    float testLen = clip.length;
                    print("Got " + clip.name + " w/Length " + clip.length);
                    StartCoroutine(PlayCutscene(testLen, "Walk"));
                    break;
                default:
                    cutsceneScreen.SetActive(false);
                    break;
            }
        } 
    }
    private IEnumerator PlayCutscene(float duration, string animName)
    {
        yield return new WaitForSeconds(duration);
        cutsceneScreenAnimator.Play(animName);
    }
}
