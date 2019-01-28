using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Engine engine = null;
	[SerializeField] private float shotForce = 50f;

	private Vector2 input;
	private Rigidbody2D rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>( );
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
	}

	public void MadeShot( Quaternion angle )
	{
		rb.AddForce( angle * Vector2.left * shotForce );
	}
}
