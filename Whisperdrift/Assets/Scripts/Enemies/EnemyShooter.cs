using UnityEngine;
using UnityEngine.Assertions;

public class EnemyShooter : MonoBehaviour
{
	[SerializeField] private Transform spawnPoint = null;
	[SerializeField] private Transform shootPoint = null;
	[SerializeField] private GameObject flash = null;
	[SerializeField] private GameObject projectile = null;
	[SerializeField] private SpriteRenderer gunSprite = null;
	[SerializeField] private Color gunColor = Color.black;
	[SerializeField] private float shotDelay = 2f;
	[SerializeField] private float shotSpreed = 5f;
	[SerializeField] private float shotDistance = 4.5f;
	[SerializeField] private float shotWarningDistance = 6f;

	private float timeToNextShot = 0;
	private GameObject player = null;

	void Start( )
	{
		if ( player == null && PlayerController.Instance != null )
			player = PlayerController.Instance.gameObject;

		//Assert.IsNotNull( player );
		Assert.IsNotNull( spawnPoint );
		Assert.IsNotNull( shootPoint );
		Assert.IsNotNull( flash );
		Assert.IsNotNull( projectile );
		Assert.IsNotNull( gunSprite );

		gunSprite.color = gunColor;
	}

	void Update( )
	{
		timeToNextShot -= Time.deltaTime;
		timeToNextShot = timeToNextShot < 0 ? 0 : timeToNextShot;

		gunColor.a = 1 - ( timeToNextShot / shotDelay );
		gunSprite.color = gunColor;

		if ( player == null && PlayerController.Instance != null )
			player = PlayerController.Instance.gameObject;
		if ( player == null || player.transform == null )
			return;

		TargetPlayer( );
		bool canShoot = CanShoot( );
		TryShoot( canShoot, timeToNextShot );
	}

	private bool CanShoot( )
	{
		// Can't shoot if out of range
		if ( Vector2.Distance( player.transform.position, transform.position ) > shotDistance )
			return false;

		// Can only shoot if has line of sight
		RaycastHit2D hit = Physics2D.Raycast( shootPoint.position, player.transform.position - shootPoint.position, 8 );

		if ( hit.collider != null )
		{
			if ( !hit.collider.CompareTag( Tags.Player ) )
			{
				gunSprite.enabled = false;
				return false;
			}
		}
		else
		{
			gunSprite.enabled = false;
			return false;
		}

		return true;
	}

	private void TargetPlayer( )
	{
		if ( Vector2.Distance( player.transform.position, transform.position ) > shotWarningDistance )
		{
			gunSprite.enabled = false;
			return;
		}

		gunSprite.enabled = true;
		Vector2 direction = player.transform.position - transform.position;
		float angle = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis( angle - 0, Vector3.forward );
		transform.rotation = rotation;
	}

	private void TryShoot( bool playerFound, float timeLeft )
	{
		if ( timeLeft > 0 || !playerFound )
			return;

		timeToNextShot = shotDelay;
		gunSprite.gameObject.transform.rotation = Quaternion.Euler( 0, 0, Random.Range( 0, 360 ) );

		Quaternion shootAngle = Quaternion.Euler( 0, 0, Random.Range( -shotSpreed, shotSpreed ) + transform.rotation.eulerAngles.z );

		Instantiate( projectile, spawnPoint.position, shootAngle );
		var f = Instantiate( flash, shootPoint.position, transform.rotation, shootPoint );
		Destroy( f, 0.4f );
	}
}
