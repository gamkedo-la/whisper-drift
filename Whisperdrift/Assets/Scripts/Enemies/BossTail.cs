using UnityEngine;

public class BossTail : MonoBehaviour
{
	[SerializeField] private GameObject explosion = null;
	[SerializeField] private float disapearTime = 3f;
	[SerializeField] private float maxSize = 3.5f;
	[SerializeField] private float hurtDisableThreshold = 0.3f;
	[SerializeField] private float deltaForTime = 0.3f;

	private float timeLeft = 0;
	private Damager damager = null;

	void Start ()
	{
		timeLeft = disapearTime;
		damager = GetComponent<Damager>( );
		BossTailSpawner.Instance.Tails.Add( gameObject );
	}

	private void OnDisable( )
	{
		if ( BossTailSpawner.Instance )
			BossTailSpawner.Instance.Tails.Remove( gameObject );
	}

	void Update ()
	{
		timeLeft -= Time.deltaTime;

		transform.localScale = Vector3.one * maxSize * ( timeLeft / disapearTime );

		if (transform.localScale.x <= hurtDisableThreshold && damager)
		{
			Destroy( damager );
			Destroy( GetComponent<Collider2D>( ) );
		}
	}

	public void SetDisapearTime( float newTime )
	{
		disapearTime = newTime + Random.Range( -newTime * deltaForTime, newTime * deltaForTime );
		timeLeft = disapearTime;
		Destroy( gameObject, disapearTime );
	}

	public void DestroyMe( float time )
	{
		Invoke( "DestroyNow", time );
	}

	public void DestroyNow( )
	{
		GameObject e = Instantiate( explosion, transform.position, Quaternion.identity );
		e.transform.localScale = transform.localScale * 2;

		Destroy( gameObject );
	}
}
