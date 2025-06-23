using System;
using UnityEngine;
using UnityEngine.UI;

public class KeybindManager : MonoBehaviour
{
    [SerializeField] private Text keybindText;
    void Start()
    {
        keybindText.text = PlayerPrefs.GetString("CustomKey");
    }
    void Update()
    {
        if(keybindText.text == "Awaiting Input")
        {
            foreach(KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode))
                {
                    keybindText.text = keyCode.ToString();
                    PlayerPrefs.SetString("CustomKey", keyCode.ToString());
                    PlayerPrefs.Save();
                }
            }
        }
    }
    public void ChangeKey()
    {
        keybindText.text = "Awaiting Input";
    }
}
