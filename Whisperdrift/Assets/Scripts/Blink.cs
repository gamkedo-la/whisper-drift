using UnityEngine;
using UnityEngine.Assertions;

public class Blink : MonoBehaviour
{
	[SerializeField] private SpriteRenderer[] spritesToBlink = null;
	[SerializeField] private Material normalMaterial = null;
	[SerializeField] private Material blinkMaterial = null;
	[SerializeField] private float blinkTime = 0.1f;

	void Start( )
	{
		if ( spritesToBlink.Length == 0 )
			spritesToBlink = GetComponentsInChildren<SpriteRenderer>( );

		Assert.IsNotNull( spritesToBlink );
		Assert.AreNotEqual( spritesToBlink.Length, 0);
		Assert.IsNotNull( normalMaterial );
		Assert.IsNotNull( blinkMaterial );
	}

	public void DoBlink( )
	{
		DoBlink( blinkTime );
	}

	public void DoBlink( float time )
	{
		SwapMaterial( blinkMaterial );

		Invoke( "Unblink", time );
	}

	private void Unblink( )
	{
		SwapMaterial( normalMaterial );
	}

	private void SwapMaterial( Material material )
	{
		foreach ( var sprite in spritesToBlink )
			sprite.material = material;
	}
}
