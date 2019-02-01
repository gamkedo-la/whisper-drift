using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float shotForce = 50f;

	private Rigidbody2D rb;
	private ZoomController zoomController;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>( );
		Assert.IsNotNull( rb );

		zoomController = FindObjectOfType<ZoomController>();
		Assert.IsNotNull( zoomController );
	}

	void FixedUpdate ()
	{
		zoomController.Zoom(rb.velocity.magnitude);
	}

	public void MadeShot( Quaternion angle )
	{
		rb.AddForce( angle * Vector2.left * shotForce );
	}
}
