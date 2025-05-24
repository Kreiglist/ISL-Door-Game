using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update() 
    {
        Vector3 screenPos = Input.mousePosition;
        // Convert screen position to world point
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, mainCamera.nearClipPlane + 10f));

        // Set gun's position — keep z consistent if needed
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
    }
}
