using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
	public static Transition Instance { get; private set; }

	private Image image;

	private void Awake()
	{
		Instance = this;
		image = GetComponentInChildren<Image>();
	}

	public void FadOut()
	{
		image.color = Color.clear;
		image.DOFade(1f, 1f).Play();
	}

	public void FadOutWhite()
	{
		image.color = new Color(1f, 1f, 1f, 0f);
		image.DOFade(1f, 0.4f).Play();
	}

	public void FadIn()
	{
		image.color = Color.black;
		image.DOFade(0f, 1f).Play();
	}
}
