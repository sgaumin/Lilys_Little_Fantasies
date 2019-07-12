using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class AudioExpress
{
	[SerializeField] private bool isUsingClips;
	[SerializeField] private AudioClip clip;
	[SerializeField] private AudioClip[] clips;
	[SerializeField] private bool attached = true;
	[SerializeField] private bool loop;
	[SerializeField] private bool isPitchModified;
	[SerializeField, Range(0f, 1f)] private float pitchMaxVariation = 0.3f;

	// ==== TODO ====
	// Play sound when scene finish - Add a DontDestroyOnLoad script
	// Destroy after time - Creation of a script
	// Create a pool of AudioSource or Stock them as a child of an empty object if not attached.

	public void Play(GameObject gameObject = null)
	{
		// Initialization
		AudioSource audioSource;
		audioSource = attached ?
			gameObject.AddComponent<AudioSource>() :
			new GameObject("Audio", typeof(AudioSource)).GetComponent<AudioSource>();

		// Setup Paramaters
		audioSource.clip = isUsingClips ? clips[Random.Range(0, clips.Length)] : clip;
		audioSource.playOnAwake = false;
		audioSource.loop = loop;
		if (isPitchModified)
		{
			audioSource.pitch -= Random.Range(0f, pitchMaxVariation);
		}

		// Play Sound
		audioSource.Play();
	}
}
