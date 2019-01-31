using UnityEngine;
using UnityEngine.Assertions;

public class RingGate : MonoBehaviour
{
	[SerializeField] private GameObject orb = null;
	[SerializeField] private SpriteRenderer[] activationGraphics = null;
	[SerializeField] private Material activationMaterial = null;

	private bool activated = false;

	void Start ()
	{
		Assert.IsNotNull( activationGraphics );
		Assert.AreNotEqual( activationGraphics.Length, 0 );

		Assert.IsNotNull( activationMaterial );

		LevelManger.Instance.AddRingGate( this );
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Player ) || !enabled || activated )
			return;

		activated = true;

		foreach ( var sprite in activationGraphics )
			sprite.material = activationMaterial;

		if ( orb )
			Destroy( orb );

		LevelManger.Instance.RingGateActiveted( this );
	}
}
