using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pic : MonoBehaviour
{
	[SerializeField] private BoxCollider2D trigger;
	[SerializeField] private BoxCollider2D collider2D;
	[SerializeField] private float dammage = 0.08f;

	private bool isFalling;
	private bool isBroken;
	private Rigidbody2D rb;
	private Animator anim;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();

		rb.gravityScale = 0f;
	}

	public void Fall()
	{
		isFalling = true;
		rb.gravityScale = 1f;
		anim.SetTrigger("Fall");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && !isFalling)
		{
			trigger.enabled = false;
			Fall();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && isFalling && !isBroken)
		{
			collision.gameObject.GetComponent<PlayerMovement>().Hitted(dammage);
			Destroy(gameObject);
		}

		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && isFalling)
		{
			anim.SetTrigger("Break");
			collider2D.enabled = false;
			rb.gravityScale = 0f;
			isBroken = true;
		}
	}
}
