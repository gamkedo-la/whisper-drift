using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Engine engine = null;
	[SerializeField] private float shotForce = 50f;
	[SerializeField] private float camAdjustmentSpeed = 3f;
	[SerializeField] private float camThreshold = 0.1f;

	private Camera cam;
	private Vector2 input;
	private Rigidbody2D rb;
	private float camSize = 3f;

	const float MIN_CAM_SIZE = 3f;
	const float MAX_CAM_SIZE = 12f;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>( );
		cam = Camera.main;
		Assert.IsNotNull( rb );
		Assert.IsNotNull( engine );
	}

	void Update ()
	{
		input.x = Input.GetAxis( "Horizontal" );
		input.y = Input.GetAxis( "Vertical" );

		input = input.magnitude > 1 ? input.normalized : input;
		engine.SetStrenght( input.magnitude );

		int sign = input.y > 0 ? 1 : -1;
		engine.transform.localRotation = Quaternion.Euler( 0, 0, (Vector2.Angle( Vector2.right, input ) + 180) * sign );

		AdjustCamera(5f + rb.velocity.magnitude);
	}

	public void MadeShot( Quaternion angle )
	{
		rb.AddForce( angle * Vector2.left * shotForce );
	}

	private void AdjustCamera( float desiredCamSize )
	{
		if ((desiredCamSize - camSize) > camThreshold ) { camSize = camSize + camAdjustmentSpeed * Time.deltaTime; }
		if ((camSize - desiredCamSize) > camThreshold ) { camSize = camSize - camAdjustmentSpeed * Time.deltaTime; }
		if (camSize < MIN_CAM_SIZE) { camSize = MIN_CAM_SIZE; }
		if (camSize > MAX_CAM_SIZE) { camSize = MAX_CAM_SIZE; }
		cam.orthographicSize = camSize;
	}
}
