using UnityEngine;
using UnityEngine.Assertions;

public class ParallaxBackground : MonoBehaviour
{
	[SerializeField] private Camera cam = null;
	[SerializeField] private Transform[] backgrounds = null;
	[SerializeField] private float[] amountsX = null;
	[SerializeField] private float[] amountsY = null;
	[SerializeField] private float speed = 1f;
	[SerializeField] private bool yScrolling = true;

	private Vector3 oldPos = Vector3.zero;

	void Start ()
	{
		Assert.IsNotNull( cam );
		Assert.IsNotNull( backgrounds );
		Assert.AreNotEqual( backgrounds.Length, 0 );
		Assert.AreEqual( amountsX.Length, backgrounds.Length );
		Assert.AreEqual( amountsY.Length, backgrounds.Length );

		oldPos = cam.transform.position;
	}

	void Update ()
	{
		for ( int i = 0; i < backgrounds.Length; i++ )
		{
			float amountX = ( oldPos.x - cam.transform.position.x ) * amountsX[i];
			float amountY = ( oldPos.y - cam.transform.position.y ) * amountsY[i];
			amountY = yScrolling ? amountY : 0;

			Vector3 newBackgroundPos = backgrounds[i].transform.position;
			newBackgroundPos.x += amountX;
			newBackgroundPos.y += amountY;

			backgrounds[i].transform.position = Vector3.Lerp
			(
				backgrounds[i].transform.position,
				newBackgroundPos,
				speed * Time.deltaTime
			);
		}

		oldPos = cam.transform.position;
	}
}
