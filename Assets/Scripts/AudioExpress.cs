using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AudioExpress : MonoBehaviour
{
	[SerializeField] private AudioClip clip;
	[SerializeField] private bool attached;

	public AudioExpress(Transform parent = null)
	{
		AudioSource audio = new AudioSource();
		GameObject.Instantiate(audio, parent);

		audio.clip = clip;
		audio.playOnAwake = false;
	}
}
