using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
	public void Attack() => PlayerMovement.Instance?.LaunchParticules();

	public void Walk() => PlayerMovement.Instance?.PlayFootSound();

	public void ResetStatus() => PlayerMovement.Instance?.ResetAttack();
}
