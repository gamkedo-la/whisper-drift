using UnityEngine;
using UnityEngine.Assertions;

public class BossCollider : MonoBehaviour
{
	[SerializeField] private GameObject smallExp = null;
	[SerializeField] private HP hp = null;
	[FMODUnity.EventRef, SerializeField] private string soundEvent = null;
	[SerializeField] private int damagePerWhisp = 1;

	private FMOD.Studio.EventInstance sound;

	void Start ()
	{
		Assert.IsNotNull( smallExp );
		Assert.IsNotNull( hp );

		sound = FMODUnity.RuntimeManager.CreateInstance( soundEvent );
	}

	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( !collision.gameObject.CompareTag( "Fairy" ) )
			return;

		sound.start( );
		hp.ChangeHP( -damagePerWhisp );
		Instantiate( smallExp, collision.gameObject.transform.position, Quaternion.identity );
		Destroy( collision.gameObject );
	}
}
