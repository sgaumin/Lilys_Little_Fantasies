using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class AudioExpress
{
	[SerializeField] private AudioClip clip;
	[SerializeField] private bool attached = true;
	[SerializeField] private bool pitchModify;
	[SerializeField, Range(0f, 1f)] private float pitchMaxVariation = 0.3f;

	// ==== TODO ====
	// Play sound when scene finish - Add a DontDestroyOnLoad script
	// Destroy after time - Creatio. of a script
	// Create a pool of AudioSource or Stock them as a child of an empty object if not attached.

	public void Play(GameObject objectAudioDemand = null)
	{
		// Initialization
		AudioSource audioSource;
		audioSource = attached ?
			objectAudioDemand.AddComponent<AudioSource>() :
			new GameObject("Audio", typeof(AudioSource)).GetComponent<AudioSource>();

		// Setup Paramaters
		audioSource.clip = clip;
		audioSource.playOnAwake = false;
		if (pitchModify)
		{
			audioSource.pitch -= Random.Range(0f, pitchMaxVariation);
		}

		// Play Sound
		audioSource.Play();
	}
}
