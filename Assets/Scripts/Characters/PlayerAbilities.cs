using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
	public void Attack()
	{
		PlayerMovement.Instance.LaunchParticules();
	}

	public void Walk()
	{
		PlayerMovement.Instance.PlayFootSound();
	}
}
