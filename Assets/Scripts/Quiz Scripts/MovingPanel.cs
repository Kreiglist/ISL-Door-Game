using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPanel : MonoBehaviour
{
    public static MovingPanel Instance;
    float duration;
    Vector3 startPos;
    Vector3 targetPos;
    float elapsedTime = 0f;
    bool movingToTarget = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        startPos = transform.position;
        targetPos = new Vector3(transform.position.x, transform.position.y, -2);  // Define the target position (keeping x & y the same, moving only in z)
    }
    public void SetTimer(float value)
    {
        duration = value;
    }
    public void ResetPanel()
    {
        elapsedTime = 0f; // Reset timer to restart movement
        movingToTarget = !movingToTarget; // Swap direction after reaching the destination
    }
    public void MovePanel()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; // Normalize time (0 to 1)
            transform.position = Vector3.Lerp(startPos, targetPos, t); // Linearly interpolate the position from startPos to targetPos based on t
        }
    }
}