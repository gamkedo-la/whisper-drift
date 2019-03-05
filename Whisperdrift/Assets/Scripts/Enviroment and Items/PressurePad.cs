using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
	[SerializeField] private UnityEvent onPressed = null;
	[SerializeField] private UnityEvent onReleased = null;
    private FMOD.Studio.EventInstance slidingDoorSound;
    private bool alreadyOpening;

    private void Awake()
    {
        alreadyOpening = false;
        slidingDoorSound = FMODUnity.RuntimeManager.CreateInstance("event:/sliding_door_open_close");
    }

    private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.PressurePlate ) )
			return;

		onPressed.Invoke( );
        if (!alreadyOpening)
        {
            slidingDoorSound.start();
            alreadyOpening = true;
        }

	}

	private void OnTriggerExit2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.PressurePlate ) )
			return;

		onReleased.Invoke( );
        slidingDoorSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        alreadyOpening = false;
	}
}
