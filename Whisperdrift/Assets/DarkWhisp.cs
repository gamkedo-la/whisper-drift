using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWhisp : MonoBehaviour
{
	private Vector3 waypoint = Vector3.zero;
	private enum Behavior { Hunting, Wandering};
	private Behavior behavior = Behavior.Hunting;
	private const float PATH_DISTANCE = 4f;
	private const float ARRIVAL_DISTANCE = 0.1f;
	private float wayPointDistance = 0f;
	private float wanderSpeed = 0.2f;
	private float huntSpeed = 1.2f;
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
		transform.Rotate(0f, 0f, 0.2f * Time.deltaTime);
	}

	void Wander() 
	{
		if (DistanceTo(waypoint) <= ARRIVAL_DISTANCE)
		{
			waypoint = GenerateNewWaypoint();
		}
		Vector2 vel = (Vector2)(waypoint - transform.position).normalized * wanderSpeed * 1000f;
		rb.AddForce(vel, ForceMode2D.Force);
	}


	float DistanceTo(Vector3 pointToCheck) 
	{
		return Vector3.Distance(transform.position, pointToCheck);
	}

	Vector3 GenerateNewWaypoint()
	{
		bool waypointIsValid = true;
		int loopCount = 0;
		Vector3 waypointGenerated = Vector3.zero;
		do
		{
			loopCount = loopCount + 1;
			float newX = Random.Range(transform.position.x - PATH_DISTANCE, transform.position.x + PATH_DISTANCE);
			float newY = Random.Range(transform.position.y - PATH_DISTANCE, transform.position.y + PATH_DISTANCE);
			waypointGenerated = new Vector3(newX, newY, transform.position.z);
			if (Physics2D.Raycast(transform.position, (waypointGenerated - transform.position), DistanceTo(waypointGenerated)))
			{ waypointIsValid = false; } else { waypointIsValid = true; }
		} while (waypointIsValid==false && loopCount < 10);
		if (waypointIsValid == true) { return waypointGenerated; }
		else return transform.position;
	}

}
