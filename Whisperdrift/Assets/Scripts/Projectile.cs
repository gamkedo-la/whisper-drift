using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float speed;
	public float lifeTime;
	public int damage;

	public GameObject explosion;
	public GameObject soundObject;

	private void Start( )
	{
		Invoke( "DestroyProjectile", lifeTime );
	}

	private void Update( )
	{
		transform.Translate( Vector2.right * speed * Time.deltaTime );
	}

	void DestroyProjectile( )
	{
		Instantiate( explosion, transform.position, Quaternion.identity );
		Destroy( gameObject );
	}

	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject.tag == "Enemy" )
			collision.gameObject.GetComponent<HP>( ).ChangeHP( -50 );

		if ( collision.gameObject.tag == "Player" )
			collision.gameObject.GetComponent<HP>( ).ChangeHP( -20 );

		DestroyProjectile( );
	}
}
