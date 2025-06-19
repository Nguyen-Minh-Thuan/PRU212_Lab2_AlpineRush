using Assets.Scripts.GameController;
using UnityEngine;

public class ChasingObstacle : MonoBehaviour
{
    private float speed; // Speed of the obstacle
    private GameConfiguration _gameConfiguration;
    public float defaultSpeed = 7f; // Default speed to reset to

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float y = 11.5f;
        transform.position = new Vector2(y, transform.position.y);
        _gameConfiguration = GetComponent<GameConfiguration>();
        _gameConfiguration._totalMupliers = 1.5f; // Reset multipliers to 1.2 at the start
    }

    // Update is called once per frame
    void Update()
    {
        speed = defaultSpeed * _gameConfiguration._totalMupliers;
        //Debug.Log($"Obstacle Speed: {speed}");
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
            //Debug.Log("Player has collided with an obstacle and lost!");
            speed = 0; // Stop the obstacle from moving
        }
    }


}
