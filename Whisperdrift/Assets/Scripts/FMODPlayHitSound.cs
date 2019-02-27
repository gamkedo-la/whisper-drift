using UnityEngine;
using UnityEngine.Assertions;

public class FMODPlayHitSound : MonoBehaviour
{
	[FMODUnity.EventRef] public string HitEvent;
	public float MinVelocity = 1f;

	private FMOD.Studio.EventInstance hit;

	private void Start( )
	{
		hit = FMODUnity.RuntimeManager.CreateInstance( HitEvent );
	}

	private void OnCollisionEnter2D( Collision2D collision )
	{
		if (collision.relativeVelocity.magnitude >= MinVelocity)
		{
			hit.start( );
		}
	}
}
