using UnityEngine;
using UnityEngine.Assertions;

public class EnemyShooter : MonoBehaviour
{
	[SerializeField] private Transform player = null;
	[SerializeField] private Transform spawnPoint = null;
	[SerializeField] private Transform shootPoint = null;
	[SerializeField] private GameObject flash = null;
	[SerializeField] private GameObject projectile = null;
	[SerializeField] private float shotDelay = 2f;
	[SerializeField] private float shotSpreed = 5f;

	private float timeToNextShot = 0;

	void Start( )
	{
		Assert.IsNotNull( player );
		Assert.IsNotNull( spawnPoint );
		Assert.IsNotNull( shootPoint );
		Assert.IsNotNull( flash );
		Assert.IsNotNull( projectile );
	}

	void Update( )
	{
		timeToNextShot -= Time.deltaTime;

		bool playerFound = FindPlayer( );
		TryShoot( playerFound, timeToNextShot );
	}

	private bool FindPlayer( )
	{
		Vector2 direction = player.position - transform.position;
		float angle = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis( angle - 0, Vector3.forward );
		transform.rotation = rotation;

		RaycastHit2D hit = Physics2D.Raycast( shootPoint.position, player.position - shootPoint.position, 8 );

		if ( hit.collider != null )
		{
			if ( !hit.collider.CompareTag( "Player" ) )
			{
				return false;
			}
		}
		else
			return false;

		return true;
	}

	private void TryShoot( bool playerFound, float timeLeft )
	{
		if ( timeLeft > 0 || !playerFound )
			return;

		timeToNextShot = shotDelay;

		Quaternion shootAngle = Quaternion.Euler( 0, 0, Random.Range( -shotSpreed, shotSpreed ) + transform.rotation.eulerAngles.z );

		Instantiate( projectile, spawnPoint.position, shootAngle );
		var f = Instantiate( flash, shootPoint.position, transform.rotation, shootPoint );
		Destroy( f, 0.4f );
	}
}
