using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Engine engine = null;
	[SerializeField] private float shotForce = 50f;
	private Camera cam;
	private Vector2 input;
	private Rigidbody2D rb;
	private float camSize=3;
	const float camAdjustmentSpeed = 3f;
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

	private void AdjustCamera(float desiredCamSize) 
	{
		if ((desiredCamSize - camSize) > 0.1f) { camSize = camSize + camAdjustmentSpeed * Time.deltaTime; }
		if ((camSize - desiredCamSize) > 0.1f) { camSize = camSize - camAdjustmentSpeed * Time.deltaTime; }
		if (camSize < MIN_CAM_SIZE) { camSize = MIN_CAM_SIZE; }
		if (camSize > MAX_CAM_SIZE) { camSize = MAX_CAM_SIZE; }
		cam.orthographicSize = camSize;
	}

	public void MadeShot( Quaternion angle )
	{
		rb.AddForce( angle * Vector2.left * shotForce );
	}
}
