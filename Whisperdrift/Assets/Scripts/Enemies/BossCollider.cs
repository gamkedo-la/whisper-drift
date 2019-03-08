using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class BossCollider : MonoBehaviour
{
	[SerializeField] private GameObject smallExp = null;
	[SerializeField] private HP hp = null;
	[FMODUnity.EventRef, SerializeField] private string soundEvent = null;
	[SerializeField] private int damagePerWhisp = 1;
	[SerializeField] private UnityEvent onFirstHit = null;

	private FMOD.Studio.EventInstance sound;
	private bool firstHit = false;

	void Start ()
	{
		Assert.IsNotNull( smallExp );
		Assert.IsNotNull( hp );

		sound = FMODUnity.RuntimeManager.CreateInstance( soundEvent );
	}

	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( !firstHit && collision.gameObject.CompareTag( "Projectile" ) )
		{
			firstHit = true;
			onFirstHit.Invoke( );
		}

		if ( !collision.gameObject.CompareTag( "Fairy" ) )
			return;

		sound.start( );
		hp.ChangeHP( -damagePerWhisp );
		Instantiate( smallExp, collision.gameObject.transform.position, Quaternion.identity );
		Destroy( collision.gameObject );
	}
}
