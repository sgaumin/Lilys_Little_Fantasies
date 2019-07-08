using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
	public static Transition Instance { get; private set; }

	[SerializeField] private Image image;

	private void Awake() => Instance = this;

	private void Start() => image = GetComponentInChildren<Image>();

	public void FadOut() => image.DOFade(1f, 1f).Play();
}
