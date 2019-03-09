using UnityEngine;
using UnityEngine.Assertions;

public class RingGate : MonoBehaviour
{
	[SerializeField] private GameObject orb = null;
	[SerializeField] private GameObject orbCatchEffect = null;
	[SerializeField] private SpriteRenderer[] activationGraphics = null;
	[SerializeField] private Material activationMaterial = null;

	private bool activated = false;
    FMOD.Studio.EventInstance RingGateParticleSpawnSound;
    FMOD.Studio.EventInstance PlayerCollidesNonDamagingObjectSound;

    void Awake()
    {
        RingGateParticleSpawnSound = FMODUnity.RuntimeManager.CreateInstance("event:/particle_spawn_sound");
        PlayerCollidesNonDamagingObjectSound = FMODUnity.RuntimeManager.CreateInstance("event:/player_collides_wall_or_platform");
    }

    void Start ()
	{
		Assert.IsNotNull( orb );
		Assert.IsNotNull( orbCatchEffect );
		Assert.IsNotNull( activationGraphics );
		Assert.AreNotEqual( activationGraphics.Length, 0 );

		Assert.IsNotNull( activationMaterial );

		LevelManger.Instance.AddRingGate( this );
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Player ) || !enabled || activated )
			return;

		activated = true;

		foreach ( var sprite in activationGraphics )
			sprite.material = activationMaterial;

		if ( orb )
		{
			RingGateParticleSpawnSound.start( );
			Instantiate( orbCatchEffect, transform.position, Quaternion.identity );
			Destroy( orb );
		}

		LevelManger.Instance.RingGateActiveted( this );
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( !collision.gameObject.CompareTag(Tags.Player) )
            return;

		//PlayerCollidesNonDamagingObjectSound.setVolume( 0.5f );
		//PlayerCollidesNonDamagingObjectSound.start();
    }

}
