using UnityEngine;
using UnityEngine.Assertions;

public class RingGate : MonoBehaviour
{
	[SerializeField] private SpriteRenderer[] activationGraphics = null;
	[SerializeField] private Material activationMaterial = null;

	void Start ()
	{
		Assert.IsNotNull( activationGraphics );
		Assert.AreNotEqual( activationGraphics.Length, 0 );

		Assert.IsNotNull( activationMaterial );
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Player ) )
			return;

		foreach ( var sprite in activationGraphics )
		{
			sprite.material = activationMaterial;
		}
	}
}
