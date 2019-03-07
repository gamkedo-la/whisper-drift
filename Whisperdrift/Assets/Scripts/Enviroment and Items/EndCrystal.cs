using UnityEngine;
using UnityEngine.Assertions;

public class EndCrystal : MonoBehaviour
{
	[SerializeField] private GameObject endEffect = null;
	[SerializeField] private GameObject music = null;
	[SerializeField] private GameObject[] parts = null;

	void Start( )
	{
		Assert.IsNotNull( endEffect );
		Assert.IsNotNull( music );
		Assert.IsNotNull( parts );
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Projectile ) && !collision.gameObject.CompareTag( Tags.Player ) )
			return;

		Destroy( );
	}

	private void Destroy( )
	{
		//Instantiate( endEffect, transform.position, Quaternion.identity );
		//Instantiate( music, transform.position, Quaternion.identity );

		for ( int i = 0; i < Random.Range( 10, 14 ); i++ )
		{
			GameObject ep = Instantiate( parts[Random.Range( 0, parts.Length )], transform.position + (Vector3)Random.insideUnitCircle * 0.2f, Quaternion.identity );
			ep.GetComponent<Rigidbody2D>( ).velocity = Quaternion.Euler( 0, 0, Random.Range( -60f, 60f ) ) * transform.up * Random.Range( 3f, 5f );
			ep.transform.localScale = new Vector3( Random.Range( 0.8f, 1.2f ), Random.Range( 0.8f, 1.2f ), 1.0f );
		}

		//GameObject.Find( "Screen Transition Out of Level" ).GetComponent<ScreenTransition>( ).StartTransition( );
		//Destroy( GameObject.FindGameObjectWithTag( "Player" ) );
		LevelManger.Instance.EndBossLevel( );
		Destroy( gameObject );
	}
}
