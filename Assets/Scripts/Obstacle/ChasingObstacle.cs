using UnityEngine;

public class ChasingObstacle : MonoBehaviour
{
    public float speed = 5f; // Speed of the obstacle

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float y = 11.5f;
        transform.position = new Vector2(y, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a PlayerController script with a method to add points
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Destroy(playerController); // Destroy the player controller to simulate losing
            }
            Debug.Log("Player has collided with an obstacle and lost!");
            speed = 0; // Stop the obstacle from moving
        }
    }


}
