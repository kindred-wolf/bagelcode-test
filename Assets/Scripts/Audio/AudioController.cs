using UnityEngine;

public class AudioController : MonoBehaviour
{

	public static AudioController Instance { get; private set; }

	[SerializeField] private AudioStorage _audioStorage;
	[SerializeField] private AudioSource _soundsAudioSource;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(this);
		}
	}

	public static void Play(AudioType audioType, bool isRandomOfType = false)
	{
		if (Instance != null)
		{
			Instance.PlayAudio(audioType, isRandomOfType);
		}
	}

	public static void StopSounds()
	{
		if (Instance != null)
		{
			Instance._soundsAudioSource.Stop();
		}
	}

	private void PlayAudio(AudioType audioType, bool isRandomOfType)
	{
		var audioToPlay = isRandomOfType ? _audioStorage.GetRandomAudioByType(audioType) :
			_audioStorage.GetFirstAudioByType(audioType);

		if (audioToPlay == null)
		{
			Debug.LogError($"[AudioController] Can't play the '{audioType}' audio type - there is no available audio clip.");
			return;
		}

		_soundsAudioSource.PlayOneShot(audioToPlay);
	}
}