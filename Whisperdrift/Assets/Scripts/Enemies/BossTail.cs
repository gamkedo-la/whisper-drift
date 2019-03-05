using UnityEngine;

public class BossTail : MonoBehaviour
{
	[SerializeField] private float disapearTime = 3f;
	[SerializeField] private float maxSize = 3.5f;
	[SerializeField] private float hurtDisableThreshold = 0.3f;

	private float timeLeft = 0;
	private Damager damager = null;

	void Start ()
	{
		timeLeft = disapearTime;
		damager = GetComponent<Damager>( );
		Destroy( gameObject, disapearTime );
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
		disapearTime = newTime;
		timeLeft = disapearTime;
	}
}
