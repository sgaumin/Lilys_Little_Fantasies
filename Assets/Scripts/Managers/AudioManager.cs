using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	[SerializeField] private AudioClip[] musics;

	private AudioSource audioSource;

	protected void Awake() => Instance = this;

	protected void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (audioSource.enabled)
		{
			audioSource.clip = musics[Random.Range(0, musics.Length)];

			if (LevelManager.Instance.LevelType == LevelTypes.Nightmare ||
				LevelManager.Instance.LevelType == LevelTypes.Bedroom)
			{
				audioSource.time = 3f;
				audioSource.volume = 0f;
				audioSource.DOFade(1f, 1f).Play();
			}

			audioSource.Play();
		}
	}

	public void FadeOutMusic() => audioSource.DOFade(0f, 1f).Play();
}
