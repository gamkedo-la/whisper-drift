using UnityEngine;

public class Damager : MonoBehaviour
{
	[SerializeField] private string reactToTag = Tags.Player;
	[SerializeField] private float hitDamage = 20f;
	[SerializeField] private float minRelativeVelocity = 0f;

	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( !collision.gameObject.CompareTag( reactToTag ) )
			return;

		if ( collision.relativeVelocity.magnitude < minRelativeVelocity )
			return;

		collision.gameObject.GetComponent<PlayerDeath>( ).Hit( collision.contacts[0].point, -collision.relativeVelocity.magnitude );
		collision.gameObject.GetComponent<HP>( ).ChangeHP( -hitDamage );
		ShakeEffect.Instance.DoSmallShake( );
	}
}
