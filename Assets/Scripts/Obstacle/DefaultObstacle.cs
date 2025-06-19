using UnityEngine;

public class DefaultObstacle : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a PlayerController script with a method to add points
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.PlayerLose(); // Call the method to handle player losing
            }
            Destroy(gameObject); // Destroy the obstacle after scoring
            Destroy(playerController);
        }
    }


}
