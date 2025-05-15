using UnityEngine;
using UnityEngine.UI;

public class ISLBook : MonoBehaviour
{
    private Image bookCover;
    private Image pageLeft;
    private Image pageRight;
    private void Awake()
    {
        bookCover = GetComponent<Image>();
        pageLeft = GetComponentInChildren<Image>();
        pageRight = GetComponentInChildren<Image>();
    }
    private void Start()
    {
        bookCover.enabled = false;
        pageLeft.enabled = false;
        pageRight.enabled = false;
    }
    public void IsBookActive()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
