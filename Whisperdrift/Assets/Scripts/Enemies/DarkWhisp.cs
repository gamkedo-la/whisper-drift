using UnityEngine;
using System.Collections.Generic;

public class DarkWhisp : MonoBehaviour
{
	[SerializeField] private GameObject spittlePrefab = null;
	[SerializeField] private AudioClip spittleSound = null;
	private Vector3 waypoint = Vector3.zero;
	private const float PATH_DISTANCE = 11f;
	private const float ARRIVAL_DISTANCE = 1f;
	///private float wayPointDistance = 0f;
	private const float DESIRED_SPEED = 1f;
	private const float DESIRED_SPEED_SQUARED = 1f;
	private const float MAX_SPEED = 5f;
	private const float MAX_SPEED_SQUARED = 25f;
	private const float WANDER_THRUST = 10f;
	private const float ANGLE_THRESHOLD = 0.02f;
	private const float ANGLE_HANDLING_AMT = 1f;
	private const float ROTATION_SPEED = 0.002f;
	private const float DRIFT_SPEED = 1f;
	private const float ATTACK_RANGE = 3f;
	private const float ATTACK_DAMAGE = 20f;
	private const float ATTACK_SHOTS = 6f;
	private float attackTimer = 0f;
	private Vector2 forwardDirection = Vector2.up;
	///private float huntSpeed = 1.2f;
	private Rigidbody2D rb;
	int layermask = 0;
	Transform player;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		waypoint = GenerateNewWaypoint();
		SetLayerMask();
		player = FindObjectOfType<PlayerController>().transform;
	}

	private void SetLayerMask()
	{
		int layermask1 = 1<<17;
		int layermask2 = 1<<16;
		int layermask3 = 1<<16;
		int layermask4 = 1<<14;
		int layermask5 = 1<<13;
		int layermask6 = 1<<11;
		int layermask7 = 1<<9;
		int layermask8 = 1<<8;
		layermask = (layermask1 | layermask2 | layermask3 | layermask4 |
					layermask5 | layermask6 | layermask7 | layermask8);
	}

	private void FixedUpdate()
	{
		if (attackTimer > 0) { attackTimer = attackTimer - Time.deltaTime; }
		if (Vector2.Distance((Vector2)transform.position, (Vector2)player.position) < ATTACK_RANGE
			&& attackTimer <= 0f)
		{
			Attack();
		}
		else { Wander(); }
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(waypoint, 0.3f);
	}

	private void Wander()
	{
		DetermineWaypoint();
		FlyTowardWaypoint();
	}

	private void DetermineWaypoint()
	{
		bool pathToWaypointIsBlocked = Physics2D.Raycast(transform.position, VectorTo(waypoint), DistanceTo(waypoint), layermask);
		if (DistanceTo(waypoint) <= ARRIVAL_DISTANCE || pathToWaypointIsBlocked == true)
		{
			waypoint = GenerateNewWaypoint();
			//DetermineWaypoint();
			return;
		}
		Debug.DrawRay(transform.position, VectorTo(waypoint), Color.green, Time.deltaTime);
	}

	/*private void RotateTowardWaypoint()
	{
		Vector2 forwardVector = transform.up;
		Vector2 waypointVector = waypoint - transform.position;
		float angle = Vector2.Angle(waypointVector, Vector2.up);
		float angleToWaypoint = Vector2.Angle(waypointVector, forwardVector);
		float maxRotationSpeed = 3f;

		float dir = Vector2.Dot(waypointVector, transform.right);
		if (dir < 0)
		{
			Debug.DrawRay(transform.position, forwardVector, Color.blue, Time.deltaTime);
			dir = -1;
		}
		else if (dir >= 0)
		{
			Debug.DrawRay(transform.position, forwardVector, Color.yellow, Time.deltaTime);
			dir = 1;
		}
		float angleToRotate = 0f;
		if (dir > 0)
		{
			angleToRotate = -angle * maxRotationSpeed * Time.fixedDeltaTime;
		}
		else if (dir < 0)
		{
			angleToRotate = angle * maxRotationSpeed * Time.fixedDeltaTime;
		}
		rb.MoveRotation(rb.rotation + angleToRotate);
		print("Angle to Waypoint: " + angle);
		print("Angle to Rotate: " + angleToRotate);
		print("Current Rotation: " + rb.rotation);
	}

	void MoveForward()
	{
		Vector2 thrustDirection =
		Vector2 force = thrustDirection * DRIFT_SPEED;
		rb.AddForce(force);
	}*/

	private void FlyTowardWaypoint()
	{
		Vector2 path = waypoint - transform.position;
		rb.MovePosition((Vector2)transform.position + (path.normalized * DRIFT_SPEED * Time.fixedDeltaTime));
		LimitSpeed();
	}

	void LimitSpeed()
	{
		if (rb.velocity.sqrMagnitude > MAX_SPEED_SQUARED)
		{
			rb.velocity = rb.velocity.normalized * MAX_SPEED;
		}
	}

	float AngleTo(Vector2 pointToCheck)
	{
		return Vector2.Angle(transform.TransformVector(transform.up), VectorTo(pointToCheck));
	}

	Vector2 VectorTo (Vector2 pointToCheck)
	{
		return pointToCheck - (Vector2)transform.position;
	}

	float DistanceTo(Vector2 pointToCheck)
	{
		return Vector2.Distance(transform.position, pointToCheck);
	}

	Vector3 GenerateNewWaypoint()
	{
		Vector3 waypointGenerated = Vector3.zero;
		float newX = Random.Range(transform.position.x - PATH_DISTANCE, transform.position.x + PATH_DISTANCE);
		float newY = Random.Range(transform.position.y - PATH_DISTANCE, transform.position.y + PATH_DISTANCE);
		waypointGenerated = new Vector3(newX, newY, transform.position.z);
		return waypointGenerated;
	}

	private void Attack()
	{
		ResetAttackTimer(6f);
		List <Vector2> targets = new List<Vector2>();

		for (int i=0;  i<ATTACK_SHOTS; i++)
		{
			float xRand = Random.Range(-3f, 3f);
			float yRand = Random.Range(-3f, 3f);
			Vector2 newLoc = new Vector2(player.position.x + xRand, player.position.y + yRand);

			targets.Add(newLoc);
			AudioSource.PlayClipAtPoint(spittleSound, (Vector2)transform.position);
		}

		foreach (Vector2 target in targets)
		{
			GameObject spittle = Instantiate(spittlePrefab, transform.position, Quaternion.identity);
			spittle.GetComponent<MoveEffect>().SetDestination(target);
			AudioSource.PlayClipAtPoint(spittleSound, (Vector2)transform.position);
		}
		player.GetComponent<HP>().ChangeHP(-ATTACK_DAMAGE);
	}

	private void ResetAttackTimer (float resetTime)
	{
		attackTimer = resetTime;
	}
}
