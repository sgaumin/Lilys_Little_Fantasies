using UnityEngine;

public class Flower : MonoBehaviour
{
	[SerializeField] private float sanityAmount = .05f;
	[SerializeField] private AudioExpress collectSound;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collectSound.Play(gameObject);
			HUD.Instance.Sanity += sanityAmount;

			Destroy(gameObject);
		}
	}
}
