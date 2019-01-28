using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
	[SerializeField] private GameObject won = null;
	[SerializeField] private Text label = null;
	[SerializeField] private GameObject[] enemies = null;

	void Start ()
	{
		Assert.IsNotNull( won );
		Assert.IsNotNull( label );
	}

	void FixedUpdate ()
	{
		CheckGameWon( );
	}

	private void CheckGameWon( )
	{
		int i = 0;
		foreach ( var e in enemies )
		{
			if ( e )
				i++;
		}

		label.text = "Enemies left: " + i;

		if ( i <= 0 )
		{
			won.SetActive( true );
			Destroy( this );
		}
	}
}
