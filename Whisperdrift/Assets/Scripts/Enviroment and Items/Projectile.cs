using UnityEngine;
using UnityEngine.Assertions;

public class Projectile : MonoBehaviour
{
	[SerializeField] private GameObject explosion = null;
	[SerializeField] private SpriteRenderer sprite = null;
	[SerializeField] private float speed = 10f;
	[SerializeField] private float lifeTime = 3f;
	[SerializeField] private float maxTravelDistance = 6f;
	[SerializeField] private int damage = 10;

	private Vector2 originPoint;
	private Rigidbody2D rb;

	void Start( )
	{
		rb = GetComponent<Rigidbody2D>( );

		Assert.IsNotNull( explosion );
		Assert.IsNotNull( sprite );
		Assert.IsNotNull( rb );

		originPoint = transform.position;
		Invoke( "DestroyProjectile", lifeTime );
	}

	void Update( )
	{
		Move( );
	}

	void FixedUpdate( )
	{
		TestDistance( );
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject.CompareTag( Tags.Enemy ) || collision.gameObject.CompareTag( Tags.Destructible ) )
			collision.gameObject.GetComponent<HP>( ).ChangeHP( -damage );

		if ( collision.gameObject.CompareTag( Tags.Player ) )
			collision.gameObject.GetComponent<HP>( ).ChangeHP( -damage );

		DestroyProjectile( );
	}

	private void Move( )
	{
		rb.MovePosition( transform.position + transform.right * speed * Time.deltaTime );
	}

	private void TestDistance( )
	{
		float distanceTraveled = Vector2.Distance( originPoint, transform.position );
		Color newColor = sprite.color;
		newColor.a = 1 - ( distanceTraveled / maxTravelDistance );
		sprite.color = newColor;

		if ( distanceTraveled > maxTravelDistance )
			DestroyProjectile( );
	}

	private void DestroyProjectile( )
	{
		Instantiate( explosion, transform.position, Quaternion.identity );
		Destroy( gameObject );
	}
}
