using UnityEngine;
using UnityEngine.Assertions;

public class PlayerDeath : MonoBehaviour
{
	public Transform respwnPoint = null;
	public GameObject explosion = null;
	public GameObject hit = null;
	public GameObject toHide = null;
	public GameObject toHide2 = null;
	public PlayerController toDisable = null;
	public Shooter toDisable2 = null;

	void Start ()
	{
		Assert.IsNotNull( respwnPoint );
	}

	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject.CompareTag( "Wall" ) )
			if ( collision.relativeVelocity.magnitude > 2 )
			{
				Instantiate( hit, collision.contacts[0].point, Quaternion.identity );
				GetComponent<HP>( ).ChangeHP( collision.relativeVelocity.magnitude * -5 );
			}

		if ( collision.gameObject.CompareTag( "Bad" ) )
			PlayerDie( );
	}

	public void PlayerDie()
	{
		toHide.SetActive( false );
		toHide2.SetActive( false );
		toDisable.enabled = false;
		toDisable2.enabled = false;
		Instantiate( explosion, transform.position, Quaternion.identity );

		Invoke( "PlayerRestart", 0.5f );
	}

	private void PlayerRestart( )
	{
		GetComponent<HP>( ).ChangeHP( 100 );
		GetComponent<Rigidbody2D>( ).velocity = Vector2.zero;
		toHide.SetActive( true );
		toHide2.SetActive( true );
		toDisable.enabled = true;
		toDisable2.enabled = true;
		transform.position = respwnPoint.position;
	}
}
