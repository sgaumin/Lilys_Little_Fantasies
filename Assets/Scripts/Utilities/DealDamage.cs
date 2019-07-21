using UnityEngine;

public class DealDamage : MonoBehaviour
{
	[SerializeField] private float dammageAmount;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<PlayerMovement>()?.Hit(dammageAmount, Vector2.zero);
			Destroy(gameObject);
		}

		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			Destroy(gameObject);
		}
	}
}
