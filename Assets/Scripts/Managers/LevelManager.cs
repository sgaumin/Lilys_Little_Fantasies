using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class LevelManager : MonoBehaviour
{
	[Header("Controllers")]
	[SerializeField] private AnimatorController bedroomAnimator;
	[SerializeField] private AnimatorController nightmareAnimator;

	private void Start()
	{
		switch (GameSystem.Instance.LevelType)
		{
			case LevelTypes.Nightmare:
				PlayerMovement.Instance.CurrentAnimatorController = nightmareAnimator;
				break;
			case LevelTypes.Bedroom:
				PlayerMovement.Instance.CurrentAnimatorController = bedroomAnimator;
				break;
		}
	}
}
