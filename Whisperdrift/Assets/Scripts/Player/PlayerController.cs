using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Slider freezBar = null;
	[SerializeField] private float shotForce = 50f;
	[SerializeField] private float freezDecline = 50f;
	[SerializeField] private float freezRegen = 10f;
	[SerializeField] private float freezMax = 100f;

	private Rigidbody2D rb;
	private ZoomController zoomController;
	private bool isFrozen = false;
	private float freezAvailable;
	private bool canFreez = true;
	private Vector2 oldVelocity;

	void Start( )
	{
		rb = GetComponent<Rigidbody2D>( );
		Assert.IsNotNull( rb );
		Assert.IsNotNull( freezBar );

		zoomController = FindObjectOfType<ZoomController>();
		Assert.IsNotNull( zoomController );

		freezAvailable = freezMax;
	}

	void Update( )
	{
		if ( isFrozen )
			freezAvailable -= freezDecline * Time.deltaTime;
		else
			freezAvailable += freezRegen * Time.deltaTime;

		freezAvailable = Mathf.Clamp( freezAvailable, 0, freezMax );

		if ( freezAvailable != freezMax )
			freezBar.gameObject.SetActive( true );
		else
			freezBar.gameObject.SetActive( false );

		freezBar.value = freezAvailable;

		if ( freezAvailable <= 0 )
		{
			canFreez = false;
			rb.velocity = oldVelocity;
		}
		else if ( freezAvailable >= freezMax )
			canFreez = true;

		if ( Input.GetMouseButtonDown( 1 ) && canFreez )
			oldVelocity = rb.velocity;

		if ( Input.GetMouseButtonUp( 1 ) && isFrozen )
		{
			canFreez = false;
			rb.velocity = oldVelocity;
		}

		if ( Input.GetMouseButton( 1 ) && canFreez )
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
