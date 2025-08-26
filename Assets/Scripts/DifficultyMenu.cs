using UnityEngine.UI;
using UnityEngine;
public class DifficultyMenu : MonoBehaviour
{
    Toggle toggle;
    void Start()
    {
        toggle = GetComponentInChildren<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleTutorial(toggle);
        });
    }
    void ToggleTutorial(Toggle change)
    {
        if(change == true) Debug.Log("Is it on?" + toggle.isOn);
        else Debug.Log("How about now?" + toggle.isOn);
    }
}
