using UnityEngine;
using UnityEngine.Assertions;

public class PlayerHP : MonoBehaviour
{
	[SerializeField] private HP hpS = null;
	[SerializeField] private GameObject hp1 = null;
	[SerializeField] private GameObject hp2 = null;
	[SerializeField] private GameObject hp3 = null;

	void Start ()
	{
		Assert.IsNotNull( hp1 );
		Assert.IsNotNull( hp2 );
		Assert.IsNotNull( hp3 );
	}

	public void OnHP ()
	{
		//Debug.Log( "Player HP: " + hpS.CurrentHP );

		switch ( hpS.CurrentHP )
		{

			case 0:
			hp1.SetActive( false );
			hp2.SetActive( false );
			hp3.SetActive( false );
			break;

			case 1:
			hp1.SetActive( false );
			hp2.SetActive( false );
			hp3.SetActive( true );
			break;

			case 2:
			hp1.SetActive( false );
			hp2.SetActive( true );
			hp3.SetActive( true );
			break;

			case 3:
			hp1.SetActive( true );
			hp2.SetActive( true );
			hp3.SetActive( true );
			break;

			default:
			break;
		}
	}
}
