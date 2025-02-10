using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Storage", menuName = "Storages/Audio Storage")]
public class AudioStorage : ScriptableObject
{
	[System.Serializable]
	private class AudioExepmlar
	{

		[SerializeField] private AudioType _audioType;
		[SerializeField] private AudioClip _audioClip;

		public AudioType AudioType => _audioType;
		public AudioClip AudioClip => _audioClip;

	}

	[SerializeField] private List<AudioExepmlar> _audioExepmlars;

	public AudioClip GetFirstAudioByType(AudioType audioType)
	{
		return _audioExepmlars.FirstOrDefault(x => x.AudioType == audioType).AudioClip;
	}

	public AudioClip GetRandomAudioByType(AudioType audioType)
	{
		var matchingExepmlars = _audioExepmlars.Where(x => x.AudioType == audioType).ToList();
		return matchingExepmlars[Random.Range(0, matchingExepmlars.Count())].AudioClip;
	}
}