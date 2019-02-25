using UnityEngine;

public class DarkWhisp : MonoBehaviour
{
	private Vector3 waypoint = Vector3.zero;
	private enum Behavior { Hunting, Wandering};
	///private Behavior behavior = Behavior.Hunting;
	private const float PATH_DISTANCE = 11f;
	private const float ARRIVAL_DISTANCE = 3f;
	///private float wayPointDistance = 0f;
	private float wanderSpeed = 0.01f;
	///private float huntSpeed = 1.2f;
	private Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		waypoint = GenerateNewWaypoint();
	}
	private void Update()
	{
		Spin();
		Wander();
	}

	void Spin()
	{
		rb.rotation += 20f;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(waypoint, 0.3f);
	}

	void Wander()
	{
		bool pathToWaypointIsBlocked = Physics2D.Raycast(transform.position, (waypoint - transform.position), DistanceTo(waypoint), 1<<17);
		if (DistanceTo(waypoint) <= ARRIVAL_DISTANCE || pathToWaypointIsBlocked==true)
		{
			waypoint = GenerateNewWaypoint();
			rb.velocity = rb.velocity.normalized * 2f;
			return;
		}
		Vector2 vel = (Vector2)(waypoint - transform.position).normalized * wanderSpeed;
		rb.AddForce(vel, ForceMode2D.Impulse);
	}


	float DistanceTo(Vector3 pointToCheck)
	{
		return Vector3.Distance(transform.position, pointToCheck);
	}

	Vector3 GenerateNewWaypoint()
	{
		Vector3 waypointGenerated = Vector3.zero;
		float newX = Random.Range(transform.position.x - PATH_DISTANCE, transform.position.x + PATH_DISTANCE);
		float newY = Random.Range(transform.position.y - PATH_DISTANCE, transform.position.y + PATH_DISTANCE);
		waypointGenerated = new Vector3(newX, newY, transform.position.z);
		return waypointGenerated;
	}

}
