using UnityEngine;

public class Lava : MonoBehaviour
{
	[SerializeField] private float forceAmount;
	[SerializeField] private float animSpeedDifference;
	[SerializeField] private float insanityAmount = 0.05f;

	private AudioSource audioSource;
	private Animator anim;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		anim = GetComponentInChildren<Animator>();
		anim.speed = Random.Range(1f - animSpeedDifference, 1f + animSpeedDifference);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
			rb?.AddForce(Vector2.up * forceAmount);

			collision.GetComponent<PlayerMovement>().Hitted(insanityAmount);
			audioSource.Play();
		}
	}
}
