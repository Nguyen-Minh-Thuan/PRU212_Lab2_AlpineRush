using UnityEngine;

public class ScoringObstacle : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a PlayerController script with a method to add points
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.AddPoints(10); // Add 10 points for hitting this obstacle
            }
            Destroy(gameObject); // Destroy the obstacle after scoring
        }
    }



}
