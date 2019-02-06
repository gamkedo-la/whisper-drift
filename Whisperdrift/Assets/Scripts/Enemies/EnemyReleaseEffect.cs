using UnityEngine;
using UnityEngine.Assertions;

public class EnemyReleaseEffect : MonoBehaviour
{
	[SerializeField] private GameObject endEffect = null;
	[SerializeField] private Light mainLight = null;
	[SerializeField] private SpriteRenderer mainSprite = null;
	[SerializeField] private ParticleSystem particles = null;
	[SerializeField] private float speed = 3f;
	[SerializeField] private float effectDuration = 3f;
	[SerializeField] private Vector2 direction = Vector2.up;

	private float timeLeft;

	void Start ()
	{
		Assert.IsNotNull( endEffect );
		Assert.IsNotNull( mainLight );
		Assert.IsNotNull( mainSprite );
		Assert.IsNotNull( particles );

		direction.Normalize( );
		timeLeft = effectDuration;
	}

	void Update ()
	{
		timeLeft -= Time.deltaTime;

		transform.position += (Vector3)direction * speed * Time.deltaTime;

		if ( timeLeft <= 0 )
			Destroy( );
	}

	private void Destroy( )
	{
		Instantiate( endEffect, transform.position, Quaternion.identity );
		Destroy( gameObject );
	}
}
