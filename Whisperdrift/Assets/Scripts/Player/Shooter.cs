using UnityEngine;
using UnityEngine.Assertions;

public class Shooter : MonoBehaviour
{
	public PlayerController player = null;
	public Transform spawnPoint = null;
	public Transform shootPoint = null;
	public GameObject flash = null;
	public GameObject projectile = null;
	public float shotDelay = 0.5f;
	public float shotSpreed = 5f;

	private float timeToNextShot = 0;

	void Start ()
	{
		Assert.IsNotNull( spawnPoint );
		Assert.IsNotNull( shootPoint );
		Assert.IsNotNull( flash );
		Assert.IsNotNull( projectile );
	}

	void Update ()
	{
		timeToNextShot -= Time.deltaTime;

		Vector2 direction = Camera.main.ScreenToWorldPoint( Input.mousePosition ) - transform.position;
		float angle = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis( angle - 0, Vector3.forward );
		transform.rotation = rotation;

		if ( timeToNextShot <= 0 && Input.GetMouseButton( 0 ) )
		{
			timeToNextShot = shotDelay;

			Quaternion shootAngle = Quaternion.Euler( 0, 0, Random.Range( -shotSpreed, shotSpreed ) + transform.rotation.eulerAngles.z );
			player.MadeShot( shootAngle );

			Instantiate( projectile, spawnPoint.position,  shootAngle );
			var f = Instantiate( flash, shootPoint.position, transform.rotation, shootPoint );
			Destroy( f, 0.4f );
		}
	}
}
