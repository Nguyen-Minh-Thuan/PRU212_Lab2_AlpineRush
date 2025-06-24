using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
	[Header("Audio Source")]
	[SerializeField] AudioSource _musicSource;
	[SerializeField] AudioSource _sfxSource;

	[Header("Audio Clip")]
	public AudioClip _background;
	public AudioClip _death;
	public AudioClip _accelerate;
	public AudioClip _sliding;
	//public AudioClip _stunts;
	public AudioClip _collects;
	public AudioClip _mainMenuMusic;


	private void Start()
	{
			_musicSource.clip = _background;
			_musicSource.Play();
	}

	public void PlaySFX(AudioClip clip)
	{
			_sfxSource.PlayOneShot(clip);
	}

	public void PlayMusic(AudioClip clip)
	{
		if (_musicSource.clip == clip) return; // Prevent restarting same music
		_musicSource.Stop();
		_musicSource.clip = clip;
		_musicSource.Play();
	}

}