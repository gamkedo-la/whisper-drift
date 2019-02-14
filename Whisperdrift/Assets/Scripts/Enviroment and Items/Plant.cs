using UnityEngine;
using UnityEngine.Assertions;

public class Plant : MonoBehaviour
{
	[SerializeField] private Animator animator = null;

	void Start ()
	{
		Assert.IsNotNull( animator );

		Invoke( "StartIdle", Random.Range( 0.1f, 2.0f ) );
	}

	void Update ()
	{

	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Projectile ) && !collision.gameObject.CompareTag( Tags.Player ) )
			return;

		animator.SetTrigger( "Interacted" );
	}

	private void StartIdle()
	{
		animator.SetTrigger( "Idle" );
	}
}
