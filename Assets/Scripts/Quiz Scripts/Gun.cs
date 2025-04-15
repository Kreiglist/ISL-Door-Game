using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject transitionScreen;
    [SerializeField] private Animator transAnimator;
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
        transitionScreen.SetActive(false);
    }
    // PUBLIC FUNCTIONS
    public void ShootAnim()
    {
        animator.SetBool("Shoot", true);
        AudioManager.instance.PlayAudio(shootSFX, transform);
        StartCoroutine(ShootAnimEnd(0.3f));
    }
    public void TransitionAnimation(bool correct)
    {
        if (correct == true)
        {
            transitionScreen.SetActive(true);
            sr.enabled = false;
            AudioManager.instance.PlayAudio(walkSFX, transform);
            StartCoroutine(TransScreen(2.063f));
        }
        else
        {
            transitionScreen.SetActive(true);
            sr.enabled = false;
            transAnimator.Play("fail");
            StartCoroutine(FailScreen(1.580f));
        }
    }
    // ENUMERATORS
    private IEnumerator ShootAnimEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("Shoot", false);
    }
    private IEnumerator TransScreen(float duration)
    {
        yield return new WaitForSeconds(duration);
        sr.enabled = true;
        transitionScreen.SetActive(false);
    }
    private IEnumerator FailScreen(float duration)
    {
        yield return new WaitForSeconds(duration);
        transitionScreen.SetActive(false);
        sr.enabled = true;
    }
}
