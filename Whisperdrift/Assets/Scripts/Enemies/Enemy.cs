using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour
{
	[SerializeField] private GameObject parent = null;
	[SerializeField] private GameObject hpBar = null;
	[SerializeField] private GameObject deathEffect = null;
	[SerializeField] private GameObject deathEffect2 = null;
	[SerializeField] private GameObject enemyPart = null;
	[SerializeField] private float touchDamage = 20f;
	[SerializeField] private GameObject[] eyes = null;
	[SerializeField] private float blinkTime = 0.05f;
	[SerializeField] private float blinkDelayMin = 3f;
	[SerializeField] private float blinkDelayMax = 5f;

	void Start ()
	{
		Assert.IsNotNull( parent );
		Assert.IsNotNull( hpBar );
		Assert.IsNotNull( deathEffect );
		Assert.IsNotNull( deathEffect2 );
		Assert.IsNotNull( enemyPart );
		Assert.IsNotNull( eyes );
		Assert.AreNotEqual( eyes.Length, 0 );

		hpBar.SetActive( false );

		Invoke( "StartBlink", Random.Range( blinkDelayMin, blinkDelayMax ) );
	}

	public void GotHit( )
	{
		if ( !hpBar.activeSelf )
			hpBar.SetActive( true );
	}

	public void OnDeath()
	{
		Instantiate( deathEffect, transform.position, Quaternion.identity );
		Instantiate( deathEffect2, transform.position, Quaternion.identity );

		for ( int i = 0; i < Random.Range( 3, 5 ); i++ )
		{
			GameObject ep = Instantiate( enemyPart, transform.position + (Vector3)Random.insideUnitCircle * 0.2f, Quaternion.identity );
			ep.GetComponent<Rigidbody2D>( ).velocity = Quaternion.Euler( 0, 0, Random.Range( -60f, 60f ) ) * transform.up * Random.Range( 2f, 4f );
			ep.transform.localScale = new Vector3( Random.Range( 0.5f, 1f ), Random.Range( 0.5f, 1f ), 1.0f );
		}

		Destroy( parent );
	}

	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Player ) )
			return;

		collision.gameObject.GetComponent<HP>( ).ChangeHP( -touchDamage );
	}

	private void StartBlink( )
	{
		foreach ( var eye in eyes )
			eye.SetActive( false );

		Invoke( "EndBlink", blinkTime );
	}

	private void EndBlink( )
	{
		foreach ( var eye in eyes )
			eye.SetActive( true );

		Invoke( "StartBlink", Random.Range( blinkDelayMin, blinkDelayMax ) );
	}
}
