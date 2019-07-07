using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
	private AudioSource audioSource;
	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			StartCoroutine(Catch());
		}
	}

	private IEnumerator Catch()
	{
		audioSource.Play();
		spriteRenderer.enabled = false;

		yield return new WaitForSeconds(2f);

		Destroy(gameObject);
	}
}
