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
		=> PlayerMovement.Instance.CurrentAnimatorController = GameSystem.Instance.LevelType == LevelTypes.Nightmare ? nightmareAnimator : bedroomAnimator;
}
