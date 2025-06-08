using UnityEngine;

public class Camera : MonoBehaviour
{
	public Transform target; // Assign player transform for tracking
	public Vector3 offset = new(0, 2, -10); // Adjust as needed
	public float smoothSpeed = 5f;

	void LateUpdate()
	{
		if (target == null) return;
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}
}
