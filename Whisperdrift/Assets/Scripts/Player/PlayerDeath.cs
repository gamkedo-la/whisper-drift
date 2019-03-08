using UnityEngine;
using UnityEngine.Assertions;

public class PlayerDeath : MonoBehaviour
{
	[SerializeField] private GameObject playerSpawn = null;
	[SerializeField] private Transform respwnPoint = null;
	[SerializeField] private GameObject explosion = null;
	[SerializeField] private GameObject hit = null;
	[SerializeField] private GameObject[] toHide = null;
	[SerializeField] private GameObject[] toDisableOnReset = null;
	[SerializeField] private Behaviour[] toDisable = null;
	[SerializeField] private TrailRenderer trailRenderer = null;
	[SerializeField] private float restartDelay = 0.5f;

	private HP hp = null;


	void Start( )
	{
		Assert.IsNotNull( respwnPoint );
		Assert.IsNotNull( explosion );
		Assert.IsNotNull( hit );
		Assert.IsNotNull( trailRenderer );
		Assert.IsNotNull( playerSpawn );

		hp = GetComponent<HP>( );
		Assert.IsNotNull( hp );
	}

	void OnEnable( )
	{
		Instantiate( playerSpawn, transform.position, Quaternion.identity );
	}

	public void Hit( Vector2 hitPosition, float magnitude )
	{
		Instantiate( hit, hitPosition, Quaternion.identity );
	}

	public void PlayerDie( )
	{
		trailRenderer.enabled = false;

		foreach ( var item in toHide )
			item.SetActive( false );

		foreach ( var item in toDisable )
			item.enabled = false;

		Instantiate( explosion, transform.position, Quaternion.identity );
		ShakeEffect.Instance.DoBigShake( );

		Invoke( "PlayerRestart", restartDelay );
	}

	private void PlayerRestart( )
	{
		hp.ChangeHP( 3 );
		GetComponent<Rigidbody2D>( ).velocity = Vector2.zero;

		foreach ( var item in toHide )
			item.SetActive( true );

		foreach ( var item in toDisableOnReset )
			item.SetActive( false );

		foreach ( var item in toDisable )
			item.enabled = true;

		transform.position = respwnPoint.position;
		trailRenderer.enabled = true;
		trailRenderer.Clear( );

		Instantiate( playerSpawn, transform.position, Quaternion.identity );
	}
}
