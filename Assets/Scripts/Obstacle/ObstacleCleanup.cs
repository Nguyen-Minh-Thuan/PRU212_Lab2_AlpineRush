using UnityEngine;

public class ObstacleCleanup : MonoBehaviour
{
	public float offsetAbovePlayer = 10f; // How far above the player before destroying
	private Transform player;

	void Start()
	{
		// Find the player by tag (as the player GameObject is tagged "Player" in Unity)
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		if (playerObj != null)
			player = playerObj.transform;
	}

	void Update()
	{
		if (player == null) return;

		// If the obstacle is above the player by offset, destroy it
		if (transform.position.y > player.position.y + offsetAbovePlayer)
		{
			Destroy(gameObject);
		}
	}
}
