using UnityEngine;
using UnityEngine.Assertions;

public class EyeBlink : MonoBehaviour
{
	[SerializeField] private GameObject[] toBlink = null;
	[SerializeField] private float blinkTime = 0.1f;
	[SerializeField] private float blinkDelta = 2f;

	void Start( )
	{
		Assert.IsNotNull( toBlink );
		Assert.AreNotEqual( toBlink.Length, 0 );

		Invoke( "Blink", Random.Range( blinkDelta, 2 * blinkDelta ) );
	}

	public void Blink( )
	{
		foreach ( var b in toBlink )
			b.SetActive( false );

		Invoke( "Unblink", blinkTime );
	}

	private void Unblink( )
	{
		foreach ( var b in toBlink )
			b.SetActive( true );

		Invoke( "Blink", Random.Range( blinkDelta, 2 * blinkDelta ) );
	}
}
