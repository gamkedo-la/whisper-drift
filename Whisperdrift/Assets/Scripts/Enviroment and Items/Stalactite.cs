using UnityEngine;
using UnityEngine.Assertions;

public class Stalactite : MonoBehaviour
{
	//[SerializeField] private GameObject endEffect = null;
	[SerializeField] private int hp = 1;
	[SerializeField] private float partsAngle = 180;
	[SerializeField] private GameObject[] parts = null;

	private int currentHp = 0;

	void Start( )
	{
		Assert.IsNotNull( parts );

		currentHp = hp;
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Projectile ) && !collision.gameObject.CompareTag( Tags.Player ) )
			return;

		currentHp--;
		if ( currentHp <= 0 )
			Destroy( );
	}

	private void Destroy( )
	{
		//Instantiate( endEffect, transform.position, Quaternion.identity );

		for ( int i = 0; i < Random.Range( 4, 6 ); i++ )
		{
			GameObject ep = Instantiate( parts[Random.Range( 0, parts.Length )], transform.position + (Vector3)Random.insideUnitCircle * 0.2f, Quaternion.identity );
			ep.GetComponent<Rigidbody2D>( ).velocity = Quaternion.Euler( 0, 0, Random.Range( -70f, 70f ) + partsAngle ) * transform.up * Random.Range( 3f, 5f );
			ep.transform.localScale = new Vector3( Random.Range( 0.3f, 0.6f ), Random.Range( 0.3f, 0.6f ), 1.0f );
			ep.transform.localRotation = Quaternion.Euler( 0, 0, Random.Range( 0, 360f ) );
			ep.GetComponent<FMODPlayHitSound>( ).on = true;
		}

		Destroy( gameObject );
	}
}
