using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
	public GameObject _settingsPanel;

	public void OpenSettings()
	{
		_settingsPanel.SetActive(true);
	}

	public void CloseSettings()
	{
		_settingsPanel.SetActive(false);
	}
}