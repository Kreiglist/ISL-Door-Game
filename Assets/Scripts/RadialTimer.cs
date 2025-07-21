using UnityEngine;
using UnityEngine.UI;

public class RadialTimer : MonoBehaviour
{
    private bool isActive = false;
    private float indicatorTimer;
    private float maxIndicatorTimer;
    private Image radialImage;
    private void Awake()
    {
        radialImage = GetComponent<Image>();
    }
    void Update()
    {
        if (isActive)
        {
            indicatorTimer -= Time.deltaTime;
            radialImage.fillAmount = (indicatorTimer / maxIndicatorTimer);

            if (indicatorTimer <= 0) StopCountdown();
        }
    }
    public void StartCountdown(float countdownTime)
    {
        isActive = true;
        maxIndicatorTimer = countdownTime;
        indicatorTimer = maxIndicatorTimer;
    }
    public void StopCountdown()
    {
        isActive = false;
    }
}