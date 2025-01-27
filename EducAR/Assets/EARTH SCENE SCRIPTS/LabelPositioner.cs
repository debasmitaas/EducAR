using UnityEngine;

public class LabelFollowPoint : MonoBehaviour
{
    public Transform targetPoint; // The point on the Earth (e.g., AsiaPoint)
    public Camera mainCamera;    // Main Camera

    private RectTransform rectTransform;

    void Start()
    {
        // Ensure the camera is assigned
        if (mainCamera == null)
            mainCamera = Camera.main;

        // Get the RectTransform of the UI element
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (targetPoint != null)
        {
            // Convert the 3D position of the target point to a 2D screen position
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetPoint.position);

            // Update the UI label position on the screen
            rectTransform.position = screenPosition;

            // Hide the label if the point is behind the camera
            rectTransform.gameObject.SetActive(screenPosition.z > 0);
        }
    }
}
