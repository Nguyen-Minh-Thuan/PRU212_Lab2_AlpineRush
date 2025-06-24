using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
	public GameObject pauseMenuUI; // Assign your pause menu UI panel in the Inspector

	private bool isPaused = false;

	public void TogglePause()
	{
		isPaused = !isPaused;
		if (isPaused)
			Pause();
		else
			Resume();
	}

	void Update()
	{
		if (Keyboard.current.escapeKey.wasPressedThisFrame)
		{
			TogglePause();
		}
	}


	public void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
	}
}
