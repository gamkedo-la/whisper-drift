using UnityEngine;
using UnityEngine.Assertions;

public class Projectile : MonoBehaviour
{
	[SerializeField] private GameObject explosion = null;
	[SerializeField] private float speed = 10f;
	[SerializeField] private float lifeTime = 3f;
	[SerializeField] private int damage = 10;

	void Start( )
	{
		Assert.IsNotNull( explosion );

		Invoke( "DestroyProjectile", lifeTime );
	}

	void Update( )
	{
		Move( );
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject.tag == "Enemy" )
			collision.gameObject.GetComponent<HP>( ).ChangeHP( -damage );

		if ( collision.gameObject.tag == "Player" )
			collision.gameObject.GetComponent<HP>( ).ChangeHP( -damage );

		DestroyProjectile( );
	}

	private void Move( )
	{
		transform.Translate( Vector2.right * speed * Time.deltaTime );
	}

	private void DestroyProjectile( )
	{
		Instantiate( explosion, transform.position, Quaternion.identity );
		Destroy( gameObject );
	}
}
