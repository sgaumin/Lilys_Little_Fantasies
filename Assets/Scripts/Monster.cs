using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	[SerializeField] private float movingDistance = 0;
	[SerializeField] private Animator animator = null;
	[SerializeField] private MonsterType mosnterType = MonsterType.Static; 	 
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag(""))
		{
			if (animator != null)
				animator.SetTrigger("Tranform"); 

			if(mosnterType != MonsterType.Vertical)
			{
				var flower = ResourceManager.Instance.GetObject(ObjectType.Flower); 
				if( flower != null )
				{
					flower.transform.position = this.transform.position; 
				}

				Object.Destroy(this.gameObject); 
			}
		}
	}
}
