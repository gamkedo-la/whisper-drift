using UnityEngine;
using UnityEngine.Assertions;

public class Faerie : MonoBehaviour
{
	[SerializeField] private Collider2D col2D = null;
	[SerializeField] private float speed = 5f;

	private Vector2 destination = Vector2.zero;

	void Start ()
	{
		Assert.IsNotNull( col2D );
	}

	void Update ()
	{
		transform.position += ( (Vector3)destination - transform.position ).normalized * speed * Time.deltaTime;

		if (Vector2.Distance( transform.position , destination) <= 0.5f)
		{
			col2D.enabled = true;
			Destroy( this );
		}
	}

	public void SetDestination( Vector2 newDestination )
	{
		destination = newDestination;
	}
}
