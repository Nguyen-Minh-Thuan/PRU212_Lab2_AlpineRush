using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame() => SceneManager.LoadScene("GameScene");
	

	public void QuitGame()
	{
		Application.Quit();
		// For debugging in the editor
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
