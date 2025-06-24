using UnityEngine;

public class SlowDownObstacle : MonoBehaviour
{

    public float slowPercent = 0.8f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Slow down the player
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SlowDown(slowPercent);
            }
        }
    }



}