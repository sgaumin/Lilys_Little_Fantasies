using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PaintScript : MonoBehaviour
{
	[SerializeField] private SpriteRenderer sprite;
	[SerializeField] private BoxCollider2D boxCollider;
	[SerializeField] private Rigidbody2D rb2D;
	[SerializeField] float force;
	[SerializeField] MovingInDirection moving;

	Tweener anim;

	protected void Start()
	{
		moving.enabled = false;
		anim = sprite.transform.DORotate(new Vector3(0, 0, 360), 0.8f, RotateMode.FastBeyond360).SetLoops(-1);
		anim.Play();
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		if (collider2D.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			rb2D.bodyType = RigidbodyType2D.Static;
			moving.enabled = true;
			anim.Pause();
			anim.Kill();
		}

		if (collider2D.gameObject.CompareTag("Destructor") || collider2D.gameObject.CompareTag("Lava"))
		{
			anim.Kill();
			Destroy(gameObject);
		}
	}

	public void Launch(Vector2 direction)
	{
		rb2D.AddForce(direction * force);
	}
}
