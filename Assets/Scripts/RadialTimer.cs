using UnityEngine;
using UnityEngine.UI;

public class RadialTimer : MonoBehaviour
{
    private bool isActive = false;
    private float indicatorTimer;
    private float maxIndicatorTimer;
    [SerializeField] private Image radialImage;

    public static RadialTimer Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
    void Update()
    {
        if (isActive == true)
        {
            indicatorTimer -= Time.deltaTime;
            radialImage.fillAmount = (indicatorTimer / maxIndicatorTimer);

            if (indicatorTimer <= 0) RestartCountdown();
        }
    }
    public void StartCountdown(float countdownTime)
    {
        isActive = true;
        maxIndicatorTimer = countdownTime;
        indicatorTimer = maxIndicatorTimer;
    }
    public void RestartCountdown()
    {
        isActive = false;
    }
}