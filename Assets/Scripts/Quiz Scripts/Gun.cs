using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip walkSFX;

    public static Gun instance;
    private Animator animator;
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
        animator = GetComponent<Animator>();
    }
    
    // PUBLIC FUNCTIONS
    public void ShootAnim()
    {
        animator.SetBool("Shoot", true);
        AudioManager.instance.PlayAudio(shootSFX, transform);
        StartCoroutine(ShootAnimEnd(0.3f));
    }
    private void TransitionAnimation(bool correct)
    {
        if (correct == true)
        {
            sr.enabled = false;
            AudioManager.instance.PlayAudio(walkSFX, transform);
            CutsceneManager.instance.CutscenePlayer("Walk");
            StartCoroutine(TransScreen(2.063f));
        }
    }
    
    // IENUMERATORS
    private IEnumerator ShootAnimEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("Shoot", false);
        TransitionAnimation(true);
    }
    private IEnumerator TransScreen(float duration)
    {
        yield return new WaitForSeconds(duration);
        CutsceneManager.instance.CutscenePlayer(default);
        sr.enabled = true;
    }
}
