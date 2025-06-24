using System;
using System.Collections;
using UnityEngine;

public class TakeFlightObstacle : MonoBehaviour
{

    public float _flightDuration = 2f; // Duration of flight in seconds
	private bool _hasTriggered = false;

	void OnTriggerEnter2D(Collider2D other)
    {
		if (_hasTriggered) return;
		if (other.CompareTag("Player"))
        {
            // Take flight
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null && playerController.IsVulnerable() == true)
            {
				_hasTriggered = true;
				StartCoroutine(HandleFlight(playerController));
            }
        }
    }

    private IEnumerator HandleFlight(PlayerController playerController)
    {
		Debug.Log("TakeFlight called");
		playerController.TakeFlight();
        yield return new WaitForSeconds(_flightDuration);
		Debug.Log("Calling Land()");
		playerController.Land();
    }

}
