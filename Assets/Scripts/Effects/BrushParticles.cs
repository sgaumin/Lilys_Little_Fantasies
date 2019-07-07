using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BrushParticles : MonoBehaviour
{
	private ParticleSystem particleSystem;

	List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
	List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

	List<ParticleCollisionEvent> collisionEvents;

	ParticleSystem.MainModule _psm_main;
	public float NO_gravity = 1f;

	List<ParticleSystem.Particle> hittedParticle = new List<ParticleSystem.Particle>();

	void OnEnable()
	{
		particleSystem = GetComponent<ParticleSystem>();
	}

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		hittedParticle.ForEach(i => i.velocity = new Vector3(-5, 0, 0));
	}

	private void OnParticleTrigger()
	{
		int numEnter = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
		int numExit = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

		for (int i = 0; i < numEnter; i++)
		{
			ParticleSystem.Particle p = enter[i];
			p.startColor = new Color32(255, 0, 0, 255);
			enter[i] = p;
			hittedParticle.Add(p);
		}
		//particleSystem.Stop();
		particleSystem.gravityModifier = 1;
		//particleSystem;
		particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
		particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
	}

	private void OnParticleCollision(GameObject other)
	{
		//int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

		int i = 0;

		Debug.Log("OnParticleCollision");
		particleSystem.gravityModifier = 0f;

		/*while (i < numCollisionEvents)
		{
			collisionEvents[i].
		}*/
	}
}
