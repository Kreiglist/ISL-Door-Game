using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;

    //[SerializeField] private GameObject menuCutscene;
    [SerializeField] private GameObject gameCutscene;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
}
