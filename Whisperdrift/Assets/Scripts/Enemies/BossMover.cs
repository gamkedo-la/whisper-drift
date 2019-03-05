using UnityEngine;
using UnityEngine.Assertions;

public class BossMover : MonoBehaviour
{
	[SerializeField] private Transform mark = null;
	[SerializeField] private Vector2? destination = null;
	[SerializeField] private Vector2 center = new Vector2(-3f, 34);
	[SerializeField] private float radius = 26f;
	[Header("Steering")]
	[SerializeField] private float speedMax = 2f;
	[SerializeField] private float steeringForceMagMax = 0.08f;

	private Vector2 velocity = Vector2.zero;

	void Start ()
	{
		Assert.IsNotNull( mark );
		mark.parent = null;

		destination = NewDestination( );
		mark.position = (Vector3)destination;
	}

	void Update ()
	{
		HaveWeArrived( );
		CalculateVelocityVector( );
		Move( );
		Rotate( );
	}

	public void SetDestination( Vector2 point )
	{
		destination = point;
		mark.position = (Vector3)destination;
	}

	private Vector2 NewDestination( )
	{
		return new Vector2
		(
			Random.Range( center.x - radius, center.x +radius ),
			Random.Range( center.y - radius, center.y + radius )
		);
	}

	private void HaveWeArrived( )
	{
		if ( destination == null ) return;

		if ( Vector2.Distance( transform.position, (Vector2)destination ) > 0.5f ) return;

		// We have arrived!
		destination = NewDestination( );
		mark.position = (Vector3)destination;
	}

	private void CalculateVelocityVector( )
	{
		if ( destination == null ) return;

		CalculateForwardVelocityVector( );
		Vector2 desiredVelocity = CalculateDesiredVelocity( );
		Vector2 steeringForce = CalculateRawSteeringForce( desiredVelocity );

		CalculateFinalVelocity( steeringForce );
	}

	private void CalculateForwardVelocityVector( )
	{
		velocity = transform.right * speedMax;
	}

	private Vector2 CalculateDesiredVelocity( )
	{
		Vector2 dir = (Vector2)destination - (Vector2)transform.position;
		Vector2 desiredVelocity = dir.normalized * speedMax;

		return desiredVelocity;
	}

	private Vector2 CalculateRawSteeringForce( Vector2 desiredVelocity )
	{
		return desiredVelocity - velocity;
	}

	private void CalculateFinalVelocity( Vector2 steeringForce )
	{
		steeringForce = steeringForce.magnitude > steeringForceMagMax ?
			steeringForce.normalized * steeringForceMagMax : steeringForce;

		velocity += steeringForce;
		velocity = velocity.magnitude > speedMax ? velocity.normalized * speedMax : velocity;
	}

	private void Move( )
	{
		if ( destination == null ) return;

		Vector2 newPos = (Vector2)transform.position + ( velocity * Time.deltaTime );
		transform.position = newPos;
	}

	private void Rotate( )
	{
		if ( velocity == Vector2.zero )
			return;

		float angle = AngleBetweenVectors( transform.position, transform.position + (Vector3)velocity );
		transform.rotation = Quaternion.Euler( 0, 0, angle );
	}

	private float AngleBetweenVectors( Vector2 vector1, Vector2 vector2 )
	{
		Vector2 diference = vector2 - vector1;
		float sign = ( vector2.y < vector1.y ) ? -1.0f : 1.0f;

		return Vector2.Angle( Vector2.right, diference ) * sign;
	}
}
