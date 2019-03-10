using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class ScreenTransition : MonoBehaviour
{
	[SerializeField] CanvasGroup canvas = null;
	[SerializeField] float speed = 1f;
	[SerializeField] bool startOnEnable = false;
	[SerializeField] bool fadeToColor = true;
	[SerializeField] UnityEvent onDone = null;

	void Start( )
	{
		Assert.IsNotNull( canvas );

		canvas.alpha = fadeToColor ? 0 : 1;
		canvas.interactable = fadeToColor ? false : true;
		canvas.blocksRaycasts = fadeToColor ? false : true;
	}

	private void OnEnable( )
	{
		if ( startOnEnable )
			StartTransition( );
	}

	public void StartTransition( )
	{
		canvas.interactable = true;
		canvas.blocksRaycasts = true;
		canvas.alpha = fadeToColor ? 0 : 1;
		StartCoroutine( "Fade" );
	}

	IEnumerator Fade( )
	{
		if ( fadeToColor )
			for ( float a = 0; a <= 1; a += Time.deltaTime * speed )
			{
				canvas.alpha = a;
				yield return null;
			}
		else
			for ( float a = 1; a > 0; a -= Time.deltaTime * speed )
			{
				canvas.alpha = a;
				yield return null;
			}

		Done( );
	}

	private void Done( )
	{
		canvas.interactable = fadeToColor ? true : false;
		canvas.blocksRaycasts = fadeToColor ? true : false;
		onDone.Invoke( );
	}
}
