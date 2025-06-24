using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
	[SerializeField] private AudioMixer _audioMixer;
	[SerializeField] private Slider _musicSlider;
	[SerializeField] private Slider _sfxSlider;

	private void Start() 
	{ 
		if (!PlayerPrefs.HasKey("MusicVolume"))
			LoadVolume();
		else 
		{ 
			SetMusicVolume(); 
			SetSFXVolume();
		}
	}
	

	public void SetMusicVolume()
	{
		float volume = _musicSlider.value;
		_audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("Music", volume);
	}

	public void SetSFXVolume()
	{
		float volume = _sfxSlider.value;
		_audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("SFX", volume);
	}

	private void LoadVolume()
	{
		_musicSlider.value = PlayerPrefs.GetFloat("Music");
		_sfxSlider.value = PlayerPrefs.GetFloat("SFX");
		SetMusicVolume();
		SetSFXVolume();
	}
}