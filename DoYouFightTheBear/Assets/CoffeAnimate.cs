using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeAnimate : MonoBehaviour
{

	Rigidbody rb;
	Vector3 old;
	public ParticleSystem coffeeParticles;
	public float rotMultiplier = 1.5f, speedMultiplier = 2;
	public AnimationCurve speedCurve;
	float particleEmitMax;

	void Awake ()
	{
		rb = GetComponent<Rigidbody> ();
		coffeeParticles.gameObject.SetActive (false);
		particleEmitMax = coffeeParticles.emission.rateOverTimeMultiplier;
	}

	void Update ()
	{
		var cm = coffeeParticles.main;
		var cs = coffeeParticles.shape;	
		var ce= coffeeParticles.emission;

		Vector3 deltaPos = (old - transform.position);
		ce.rateOverTimeMultiplier = deltaPos.magnitude > 0 ? particleEmitMax : 0;
		cs.rotation = (new Vector3(deltaPos.x, deltaPos.z, 0)*rotMultiplier);
		cm.startSpeedMultiplier = speedCurve.Evaluate(deltaPos.magnitude) * speedMultiplier;
		old = transform.position;
	}

	void TurnOnPartilces(){
		coffeeParticles.gameObject.SetActive (true);
	}
}
