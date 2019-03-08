using UnityEngine;
using UnityEngine.Assertions;

public class EnableAfterDelay : MonoBehaviour
{
	[SerializeField] private GameObject toEnable = null;
	[SerializeField] private float delay = 1f;

	void Start ()
	{
		Assert.IsNotNull( toEnable );

		Invoke( "EnableNow", delay );
	}

	private void EnableNow ()
	{
		toEnable.SetActive( true );
	}
}
