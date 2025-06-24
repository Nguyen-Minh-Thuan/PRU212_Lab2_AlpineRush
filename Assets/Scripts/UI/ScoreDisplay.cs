using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
	public PlayerController playerController;
	public TextMeshProUGUI scoreText;

	void Update()
	{
		if (playerController != null && scoreText != null)
		{
			scoreText.text = playerController.GetPlayerPoints().ToString("0");
		}
	}
}