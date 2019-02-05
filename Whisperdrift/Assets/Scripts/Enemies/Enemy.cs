using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour
{
	[SerializeField] private GameObject parent = null;
	[SerializeField] private GameObject hpBar = null;
	[SerializeField] private GameObject deathEffect = null;
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
