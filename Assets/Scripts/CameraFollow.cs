using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset;  
    public float smoothSpeed = 0.125f; 

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
