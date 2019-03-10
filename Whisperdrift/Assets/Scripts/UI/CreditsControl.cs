using UnityEngine;
using UnityEngine.Assertions;

public class CreditsControl : MonoBehaviour
{
	[SerializeField] private Transform creditText = null;
	[SerializeField] private ScreenTransition screenTransition = null;
	[SerializeField] private Animator animator = null;
	[SerializeField] private float scrollSpeed = 5f;

	void Start ()
	{
		Assert.IsNotNull( creditText );
		Assert.IsNotNull( screenTransition );
		Assert.IsNotNull( animator );
	}

	void Update ()
	{
		if ( Input.GetAxis("Vertical") != 0 )
		{
			if ( animator.enabled )
			{
				animator.StopPlayback( );
				animator.enabled = false;
			}

			Vector2 pos = creditText.position;
			pos.y += Input.GetAxis( "Vertical" ) * scrollSpeed * Time.deltaTime;
			creditText.position = pos;
		}
	}

	public void EndCredits()
	{
		screenTransition.StartTransition( );
	}
}
