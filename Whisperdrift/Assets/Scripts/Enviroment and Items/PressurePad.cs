using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
	[SerializeField] private UnityEvent onPressed = null;
	[SerializeField] private UnityEvent onReleased = null;
    
    private bool alreadyReleased;
    FMOD.Studio.EventInstance PressureReleaseSound;

    private void Awake()
    {
        alreadyReleased = false;
        PressureReleaseSound = FMODUnity.RuntimeManager.CreateInstance("event:/sliding_door_pressure_release_sound");
    }

    private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.PressurePlate ) )
			return;

        if (!alreadyReleased)
        {
            alreadyReleased = true;
            PressureReleaseSound.start();
        }

		onPressed.Invoke( );

	}

	private void OnTriggerExit2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.PressurePlate ) )
			return;

		onReleased.Invoke( );
        
	}
}
