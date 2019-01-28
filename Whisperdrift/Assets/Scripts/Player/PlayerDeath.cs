using UnityEngine;
using UnityEngine.Assertions;

public class PlayerDeath : MonoBehaviour
{
	[SerializeField] private Transform respwnPoint = null;
	[SerializeField] private GameObject explosion = null;
	[SerializeField] private GameObject hit = null;
	[SerializeField] private GameObject[] toHide = null;
	[SerializeField] private Behaviour[] toDisable = null;
	[SerializeField] private TrailRenderer trailRenderer = null;
	[SerializeField] private float restartDelay = 0.5f;


	void Start ()
	{
		Assert.IsNotNull( respwnPoint );
		Assert.IsNotNull( explosion );
		Assert.IsNotNull( hit );
		Assert.IsNotNull( trailRenderer );
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
		trailRenderer.enabled = false;

		foreach ( var item in toHide )
			item.SetActive( false );

		foreach ( var item in toDisable )
			item.enabled = false;

		Instantiate( explosion, transform.position, Quaternion.identity );

		Invoke( "PlayerRestart", restartDelay );
	}

	private void PlayerRestart( )
	{
		GetComponent<HP>( ).ChangeHP( 100 );
		GetComponent<Rigidbody2D>( ).velocity = Vector2.zero;

		foreach ( var item in toHide )
			item.SetActive( true );

		foreach ( var item in toDisable )
			item.enabled = true;

		transform.position = respwnPoint.position;
		trailRenderer.enabled = true;
		trailRenderer.Clear();
	}
}
