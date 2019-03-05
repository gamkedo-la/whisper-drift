using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ButtonToScene : MonoBehaviour
{
	public string sceneName = "null";
	public bool startTime = false;
	public AudioClip clickSound;
	public UnityEvent OnButtonPress;

	private AudioSource aud = null;

	void Start( )
	{
		aud = GetComponent<AudioSource>( );
		if ( aud == null )
			aud = FindObjectOfType<AudioSource>( );
	}

	void OnMouseOver( )
	{
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			if ( aud != null )
				aud.PlayOneShot( clickSound );

			if ( sceneName == "Quit" )
				Application.Quit( );
			else if ( sceneName == "Reset" )
				SceneManager.LoadScene( gameObject.scene.name );
			else
				OnButtonPress.Invoke( );

			if ( startTime )
				Time.timeScale = 1f;
		}
	}

	public void GoToScene( )
	{
		SceneManager.LoadScene( sceneName );
	}
}
