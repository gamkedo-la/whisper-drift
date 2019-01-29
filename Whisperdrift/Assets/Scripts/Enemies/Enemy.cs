using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour
{
	[SerializeField] private GameObject parent = null;
	[SerializeField] private GameObject hpBar = null;
	[SerializeField] private GameObject deathEffect = null;

	void Start ()
	{
		Assert.IsNotNull( parent );
		Assert.IsNotNull( hpBar );
		Assert.IsNotNull( deathEffect );

		hpBar.SetActive( false );
	}

	void Update ()
	{

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
		if ( !collision.gameObject.CompareTag( "Player" ) )
			return;

		Debug.Log( "Player collided with " + name );
	}
}
