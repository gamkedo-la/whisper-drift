using UnityEngine;
using UnityEngine.Assertions;

public class RingGateExit : MonoBehaviour
{
	[SerializeField] private SpriteRenderer[] activationGraphics = null;
	[SerializeField] private GameObject[] objectsToActivate = null;
	[SerializeField] private Material activationMaterial = null;

	private bool activated = false;
	private bool poweredUp = false;

    FMOD.Studio.EventInstance RingGateExitActivationLowPulseSound;
    FMOD.Studio.EventInstance RingGateExitActivationSlidingChordSound;

    void Awake() //FMOD's built in init function to hook up sounds??? I think???
    {
        RingGateExitActivationLowPulseSound = FMODUnity.RuntimeManager.CreateInstance("event:/exit_activate_low_end_pulse");
        RingGateExitActivationSlidingChordSound = FMODUnity.RuntimeManager.CreateInstance("event:/exit_activate_chord_slide");
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
}
