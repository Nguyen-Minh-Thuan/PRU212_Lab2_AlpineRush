using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
	void Start()
	{
		AudioManager audioManager = FindFirstObjectByType<AudioManager>();
		Debug.Log("MainMenuMusic Start, AudioManager: " + audioManager);
		if (audioManager != null && audioManager._mainMenuMusic != null)
		{
			Debug.Log("Playing main menu music: " + audioManager._mainMenuMusic.name);
			audioManager.PlayMusic(audioManager._mainMenuMusic);
		}
		else
		{
			Debug.LogWarning("Main menu music AudioClip not assigned!");
		}
	}

}
