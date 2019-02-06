using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Slider freezeBar = null;
	[SerializeField] private float shotForce = 50f;
	[SerializeField] private float freezeDecline = 50f;
	[SerializeField] private float freezeRegen = 10f;
	[SerializeField] private float freezeMax = 100f;

	private Rigidbody2D rb;
	private ZoomController zoomController;
	private bool isFrozen = false;
	private float freezeAvailable;
	private bool canFreeze = true;
	private Vector2 oldVelocity;

	void Start( )
	{
		rb = GetComponent<Rigidbody2D>( );
		Assert.IsNotNull( rb );
		Assert.IsNotNull( freezeBar );

		zoomController = FindObjectOfType<ZoomController>();
		Assert.IsNotNull( zoomController );

		freezeAvailable = freezeMax;
	}

	void Update( )
	{
		if ( isFrozen )
			freezeAvailable -= freezeDecline * Time.deltaTime;
		else
			freezeAvailable += freezeRegen * Time.deltaTime;

		freezeAvailable = Mathf.Clamp( freezeAvailable, 0, freezeMax );

		if ( freezeAvailable != freezeMax )
			freezeBar.gameObject.SetActive( true );
		else
			freezeBar.gameObject.SetActive( false );

		freezeBar.value = freezeAvailable;

		if ( freezeAvailable <= 0 )
		{
			canFreeze = false;
			rb.velocity = oldVelocity;
		}
		else if ( freezeAvailable >= freezeMax )
			canFreeze = true;

		if ( Input.GetMouseButtonDown( 1 ) && canFreeze )
			oldVelocity = rb.velocity;

		if ( Input.GetMouseButtonUp( 1 ) && isFrozen )
		{
			canFreeze = false;
			rb.velocity = oldVelocity;
		}

		if ( Input.GetMouseButton( 1 ) && canFreeze )
			isFrozen = true;
		else
			isFrozen = false;
	}

	void FixedUpdate( )
	{
		if ( !isFrozen )
			zoomController.Zoom(rb.velocity.magnitude);

		if ( isFrozen )
			rb.velocity = Vector2.zero;
	}

	public void MadeShot( Quaternion angle )
	{
		if ( !isFrozen )
			rb.AddForce( angle * Vector2.left * shotForce );
	}
}
