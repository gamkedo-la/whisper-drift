using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
	[SerializeField] private UnityEvent onPressed = null;
	[SerializeField] private UnityEvent onReleased = null;
    
    

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.PressurePlate ) )
			return;

        

		onPressed.Invoke( );

	}

	private void OnTriggerExit2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.PressurePlate ) )
			return;

		onReleased.Invoke( );
        
	}
}
