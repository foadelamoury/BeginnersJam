using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // Reference to the player's transform
    public Vector3 offset;     // Offset from the player
    public float smoothSpeed = 0.125f; // Smooth movement speed

    private float minX, maxX, maxY, minY; // Dynamic boundary limits

    void Start()
    {
        CalculateCameraBounds();
        if (PlayerPrefs.GetInt("PlayerScore") == 0)
        {
            PlayerPrefs.SetInt("PlayerScore", 0);
            PlayerPrefs.Save();
        }

    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Clamp the camera's position within the calculated boundaries
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);
        transform.position = smoothedPosition;
    }

    void CalculateCameraBounds()
    {
        Camera cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("CameraFollow script must be attached to a camera!");
            return;
        }

        float cameraHalfWidth = cam.orthographicSize * cam.aspect; 

        float backgroundLeftEdge = 6.8f;  
        float backgroundRightEdge = 351f;
        float backgroundTopEdge = 41.4f;
        float backgroundBottomEdge = -34f;
        // Calculate dynamic min and max X boundaries
        minX = backgroundLeftEdge + cameraHalfWidth;
        maxX = backgroundRightEdge - cameraHalfWidth;
        maxY = backgroundTopEdge - cam.orthographicSize;
        minY = backgroundBottomEdge + cam.orthographicSize;
    }
    void OnValidate()
    {
        // Recalculate bounds if values are changed in the inspector
        CalculateCameraBounds();
    }
}
