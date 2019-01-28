using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
	public GameObject won;
	public Text label;
	public GameObject[] enemies;

	void Start ()
	{
		Assert.IsNotNull( won );
	}

	void FixedUpdate ()
	{
		int i = 0;
		foreach ( var e in enemies )
		{
			if ( e )
				i++;
		}

		label.text = "Enemies left: " + i;

		if (i <= 0)
		{
			won.SetActive( true );
			Destroy( this );
		}
	}
}
