using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintScript : MonoBehaviour
{
	[SerializeField] private SpriteRenderer sprite;
	[SerializeField] private BoxCollider2D collider;

	private bool isMoving;
	private float time;
	// Start is called before the first frame update
	void Start()
    {
		time = 0;
		isMoving = true;

	}

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;
		if (isMoving)
		{
			this.gameObject.transform.position += new Vector3(3f, 0.5f, 0f) * Time.deltaTime;
			this.gameObject.transform.Rotate(new Vector3(0f, -90 / Time.deltaTime));
		}
	}

	private void OnTriggerCollider2D(GameObject gameObject)
	{
		isMoving = false;
		if (gameObject.CompareTag("Lava"))
		{
			this.enabled = false;
		}
		if (gameObject.CompareTag("Monsters"))//or get component sur script
		{
			//gameObject.Hit();
			this.enabled = false;
		}
	}
}
