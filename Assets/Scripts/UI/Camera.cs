using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Target Player
    public Vector3 offset = new Vector3(0, 2, -10); // Distance of camera and target
    public float smoothSpeed = 5f;

    public float minX = -22f;
    public float maxX = 22f;

    void LateUpdate()
    {
        if (target == null) return;

        // Position camera based on target position and offset
        Vector3 desiredPosition = target.position + offset;

        // smooth Animation
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        //Limit camera
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);

        transform.position = smoothedPosition;
    }
}
