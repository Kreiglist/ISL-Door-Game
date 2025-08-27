using UnityEngine.UI;
using UnityEngine;
public class DifficultyMenu : MonoBehaviour
{
    public static DifficultyMenu instance;
    Toggle toggle;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        toggle = GetComponentInChildren<Toggle>(true);
        toggle.onValueChanged.AddListener(delegate (bool value) {
            ToggleTutorial(value);
        });
    }
    void ToggleTutorial(bool change)
    {
        if (change == true)
        {
            MainMenu.instance.toTutorial = true;
            Debug.Log("Is it on?" + toggle.isOn);
        }
        else
        {
            MainMenu.instance.toTutorial = false;
            Debug.Log("How about now?" + toggle.isOn);
        }
    }
}
