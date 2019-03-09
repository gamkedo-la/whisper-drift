using UnityEngine;

public class FMODPlayHitSound : MonoBehaviour
{
	[FMODUnity.EventRef] public string HitEvent;
	public float MinVelocity = 1f;
	public bool on = false;

	private FMOD.Studio.EventInstance hit;

	private void Start( )
	{
		hit = FMODUnity.RuntimeManager.CreateInstance( HitEvent );
		Invoke( "On", 10f );
	}

	private void On( ) => on = true;


	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.relativeVelocity.magnitude >= MinVelocity && on )
		{
			hit.start( );
		}
	}
}
