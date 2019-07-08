using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class OutroManager : MonoBehaviour
{
	[SerializeField] private float durationBeforeLoad;

	private PlayableDirector intro;

	private void Start()
	{
		intro = GetComponent<PlayableDirector>();

		intro.Play();
		StartCoroutine(LoadNext());
	}

	private IEnumerator LoadNext()
	{
		yield return new WaitForSeconds(durationBeforeLoad);
		LevelLoader.Instance.LoadStartScene();
	}
}
