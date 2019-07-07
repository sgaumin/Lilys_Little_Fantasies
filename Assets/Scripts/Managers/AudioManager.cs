using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioClip[] musics;

	private AudioSource audioSource;

	protected void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = musics[Random.Range(0, musics.Length)];
		audioSource.Play();
	}
}
