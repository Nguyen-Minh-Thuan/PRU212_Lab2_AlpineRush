using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
	public GameObject gameOverPanel; // Assign in Inspector

	public void ShowGameOver()
	{
		gameOverPanel.SetActive(true);
		Time.timeScale = 0f; // Pause the game
	}

	public void Restart()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void QuitToMenu()
	{
		Time.timeScale = 1f;
		// Replace "MainMenu" with your main menu scene name
		SceneManager.LoadScene("MainMenu");
	}
}