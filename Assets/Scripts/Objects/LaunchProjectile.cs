using System.Collections;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
	[SerializeField] private DealDamage projectilePrefab;
	[SerializeField] private Transform spawn;
	[SerializeField, Range(0f, 2f)] private float timeToShoot = 1.5f;
	[SerializeField, Range(50, 600)] private float forceAmount = 300;
	[SerializeField, Range(50, 600)] private float torqueAmount = 200f;

	private void Start() => StartCoroutine(Launch());

	private IEnumerator Launch()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeToShoot);

			DealDamage projectileTemp = Instantiate(projectilePrefab, spawn);

			float factor = Random.value > 0.5f ? Random.Range(0.5f, 1f) : -Random.Range(0.5f, 1f);

			Vector2 direction = (Vector2.up + Vector2.right * factor) * forceAmount;

			Rigidbody2D rb = projectileTemp.GetComponent<Rigidbody2D>();

			rb.AddForce(direction);
			rb.AddTorque(torqueAmount);
		}
	}
}
