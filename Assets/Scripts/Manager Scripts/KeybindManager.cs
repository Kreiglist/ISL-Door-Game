using System;
using UnityEngine;
using UnityEngine.UI;

public class KeybindManager : MonoBehaviour
{
    [SerializeField] private Text keybindTextBook;

    void Start()
    {
        keybindTextBook.text = PlayerPrefs.GetString("CustomKey");
    }
    void Update()
    {
        UpdateKeyBook();
    }
    public void ChangeKeyBook()
    {
        keybindTextBook.text = "Awaiting Input";
    }
    private void UpdateKeyBook()
    {
        if (keybindTextBook.text == "Awaiting Input")
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode))
                {
                    keybindTextBook.text = keyCode.ToString();
                    PlayerPrefs.SetString("CustomKey", keyCode.ToString());
                    PlayerPrefs.Save();
                }
            }
        }
    }
}
