using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffect : MonoBehaviour
{
	public Vector2 destination;
	private float speed = 10;
	private const float ARRIVAL_THRESHOLD = 0.1f;

	private void Update()
	{
		Vector2 movement = (destination - (Vector2)transform.position).normalized * speed * Time.deltaTime;
		if (Vector2.Distance((Vector2)transform.position, destination) < ARRIVAL_THRESHOLD)
		{
			Destroy(this.gameObject);
		}
		else 
		{
			transform.position = transform.position + (Vector3)movement;
		}
	}

	public void SetDestination (Vector2 receivedLocation)
	{
		destination = receivedLocation;
	}
}
