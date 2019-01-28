using UnityEngine;
using UnityEngine.Assertions;

public class Engine : MonoBehaviour
{
	public ParticleSystem particles;
	public float maxParticles = 50f;

	void Start ()
	{
		Assert.IsNotNull( particles );
	}

	public void SetStrenght ( float value )
	{
		var e = particles.emission;
		e.rateOverTime = maxParticles * value;
	}
}
