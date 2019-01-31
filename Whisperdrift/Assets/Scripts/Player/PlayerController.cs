using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float shotForce = 50f;

	private Rigidbody2D rb;
	private ZoomController zoomController;
	//private float speedometer = 0f;
	//private float speedDelta = 0f;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>( );
		Assert.IsNotNull( rb );

		zoomController = FindObjectOfType<ZoomController>();
		Assert.IsNotNull( zoomController );
	}

	void FixedUpdate ()
	{
		//speedDelta = rb.velocity.magnitude - speedometer;
		zoomController.Zoom(rb.velocity.magnitude);
		//speedometer = speedometer + speedDelta;
	}

	public void MadeShot( Quaternion angle )
	{
		rb.AddForce( angle * Vector2.left * shotForce );
	}

}
