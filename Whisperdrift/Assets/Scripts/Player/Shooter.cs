using UnityEngine;
using UnityEngine.Assertions;

public class Shooter : MonoBehaviour
{
	[SerializeField] private PlayerController player = null;
	[SerializeField] private Transform spawnPoint = null;
	[SerializeField] private Transform shootPoint = null;
	[SerializeField] private GameObject flash = null;
	[SerializeField] private GameObject projectile = null;
	[SerializeField] private float shotDelay = 0.5f;
	[SerializeField] private float shotSpreed = 5f;

	private float timeToNextShot = 0;

	void Start( )
	{
		Assert.IsNotNull( spawnPoint );
		Assert.IsNotNull( shootPoint );
		Assert.IsNotNull( flash );
		Assert.IsNotNull( projectile );
	}

	void Update( )
	{
		timeToNextShot -= Time.deltaTime;

		Rotate( );
		TryToShoot( );
	}

	private void Rotate( )
	{
		Vector2 direction = Camera.main.ScreenToWorldPoint( Input.mousePosition ) - transform.position;
		float angle = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis( angle - 0, Vector3.forward );
		transform.rotation = rotation;
	}

	private void TryToShoot( )
	{
		if ( timeToNextShot > 0 || !Input.GetMouseButton( 0 ) )
			return;

		timeToNextShot = shotDelay;

		Quaternion shootAngle = Quaternion.Euler( 0, 0, Random.Range( -shotSpreed, shotSpreed ) + transform.rotation.eulerAngles.z );
		player.MadeShot( shootAngle );

		Instantiate( projectile, spawnPoint.position, shootAngle );
		var f = Instantiate( flash, shootPoint.position, transform.rotation, shootPoint );
		Destroy( f, 0.4f );
	}
}
