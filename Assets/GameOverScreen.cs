using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
	[SerializeField] private float durationBeforeLoad;

	private void Start()
	{
		StartCoroutine(GameOver());
	}

	private IEnumerator GameOver()
	{
		yield return new WaitForSeconds(durationBeforeLoad);
		LevelLoader.Instance.LoadStartScene();
	}
}
