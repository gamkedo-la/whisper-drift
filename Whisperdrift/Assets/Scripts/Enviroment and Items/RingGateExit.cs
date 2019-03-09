﻿using UnityEngine;
using UnityEngine.Assertions;

public class RingGateExit : MonoBehaviour
{
	[SerializeField] private SpriteRenderer[] activationGraphics = null;
	[SerializeField] private GameObject[] objectsToActivate = null;
	[SerializeField] private Material activationMaterial = null;

	private bool activated = false;
	private bool poweredUp = false;
    private float timer = 0.0f;
    private float waitTime = 0.125f;

    FMOD.Studio.EventInstance RingGateExitActivationLowPulseSound;
    FMOD.Studio.EventInstance RingGateExitActivationSlidingChordSound;
    FMOD.Studio.EventInstance RingGateParticleSpawnSound;
    FMOD.Studio.EventInstance PlayerCollidesNonDamagingObjectSound;


    void Awake()
    {
        RingGateExitActivationLowPulseSound = FMODUnity.RuntimeManager.CreateInstance("event:/exit_activate_low_end_pulse");
        RingGateExitActivationSlidingChordSound = FMODUnity.RuntimeManager.CreateInstance("event:/exit_activate_chord_slide");
        RingGateParticleSpawnSound = FMODUnity.RuntimeManager.CreateInstance("event:/particle_spawn_sound");
        PlayerCollidesNonDamagingObjectSound = FMODUnity.RuntimeManager.CreateInstance("event:/player_collides_wall_or_platform");
    }

	void Start( )
	{
		Assert.IsNotNull( activationGraphics );
		Assert.AreNotEqual( activationGraphics.Length, 0 );
		Assert.IsNotNull( objectsToActivate );
		Assert.AreNotEqual( objectsToActivate.Length, 0 );

		Assert.IsNotNull( activationMaterial );

		LevelManger.Instance.AddRingGateExit( this );
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Player ) || !enabled || activated || !poweredUp )
			return;

		activated = true;
        RingGateExitActivationLowPulseSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        LevelManger.Instance.ExitActivated( );
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Player))
            return;

		//PlayerCollidesNonDamagingObjectSound.setVolume( 0.5f );
		//PlayerCollidesNonDamagingObjectSound.start();
    }

    public void PowerUp( )
	{
		poweredUp = true;
        RingGateExitActivationLowPulseSound.start();
        RingGateExitActivationSlidingChordSound.start();

        foreach ( var sprite in activationGraphics )
			sprite.material = activationMaterial;

		foreach ( var go in objectsToActivate )
			go.SetActive(true);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitTime && poweredUp && !activated)
        {
            RingGateParticleSpawnSound.start();
            timer = 0;
        }
    }
}
