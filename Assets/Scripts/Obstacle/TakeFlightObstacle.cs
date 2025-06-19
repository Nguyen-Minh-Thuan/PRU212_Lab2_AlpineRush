using System;
using System.Collections;
using UnityEngine;

public class TakeFlightObstacle : MonoBehaviour
{

    public float _flightDuration = 2f; // Duration of flight in seconds

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Take flight
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null && playerController.IsVulnerable() == true)
            {
                StartCoroutine(HandleFlight(playerController));
            }
        }
    }

    private IEnumerator HandleFlight(PlayerController playerController)
    {
        playerController.TakeFlight();
        yield return new WaitForSeconds(_flightDuration);
        playerController.Land();
    }

}
