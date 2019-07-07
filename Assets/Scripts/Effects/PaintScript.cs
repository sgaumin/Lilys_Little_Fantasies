using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PaintScript : MonoBehaviour
{
	[SerializeField] private SpriteRenderer sprite;
	[SerializeField] private BoxCollider2D collider;
	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] float force;
	[SerializeField] MovingInDirection moving;

	private bool isMoving;
	private float time;
	Tweener anim;
	// Start is called before the first frame update
	void Start()
    {
		time = 0;
		isMoving = true;
		moving.enabled = false;
		anim = sprite.transform.DORotate(new Vector3(0, 0, 360), 0.8f, RotateMode.FastBeyond360).SetLoops(-1);
		anim.Play();
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		if (collider2D.gameObject.layer==LayerMask.NameToLayer("Ground"))
		{
			rigidbody2D.bodyType = RigidbodyType2D.Static;
			moving.enabled = true;
			anim.Pause();
			anim.Kill();
		}

		if (collider2D.gameObject.CompareTag("Destructor")||collider2D.gameObject.CompareTag("Lava"))
		{
			anim.Kill();
			Destroy(gameObject);
		}
		/*if (gameObject.CompareTag("Lava"))
		{
			this.enabled = false;
		}
		if (gameObject.CompareTag("Monsters"))//or get component sur script
		{
			//gameObject.Hit();
			this.enabled = false;
		}*/
	}

	public void Launch(Vector2 direction)
	{
		rigidbody2D.AddForce(direction*force);
	}
}
