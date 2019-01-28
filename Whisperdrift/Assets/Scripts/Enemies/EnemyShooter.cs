using UnityEngine;
using UnityEngine.Assertions;

public class EnemyShooter : MonoBehaviour
{
	public Transform player = null;
	public Transform spawnPoint = null;
	public Transform shootPoint = null;
	public GameObject flash = null;
	public GameObject projectile = null;
	public float shotDelay = 2f;
	public float shotSpreed = 5f;

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

		Vector2 direction = player.position - transform.position;
		float angle = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis( angle - 0, Vector3.forward );
		transform.rotation = rotation;

		RaycastHit2D hit = Physics2D.Raycast( shootPoint.position, player.position - shootPoint.position, 8 );

		if ( hit.collider != null )
		{
			if ( !hit.collider.CompareTag( "Player" ) )
			{
				return;
			}
		}
		else
			return;

		if ( timeToNextShot <= 0 )
		{
			timeToNextShot = shotDelay;

			Quaternion shootAngle = Quaternion.Euler( 0, 0, Random.Range( -shotSpreed, shotSpreed ) + transform.rotation.eulerAngles.z );

			Instantiate( projectile, spawnPoint.position, shootAngle );
			var f = Instantiate( flash, shootPoint.position, transform.rotation, shootPoint );
			Destroy( f, 0.4f );
		}
	}
}
