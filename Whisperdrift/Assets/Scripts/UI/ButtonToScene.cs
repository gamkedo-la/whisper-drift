using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonToScene : MonoBehaviour, IPointerClickHandler
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

	void ClickEvent()
	{
		if ( sceneName == "Quit" )
		{
			Application.Quit( );
		}
		else if ( sceneName == "Reset" )
		{
			Time.timeScale = 1f;
			SceneManager.LoadScene( gameObject.scene.name );
		}
		else
		{
			Time.timeScale = 1f;
			OnButtonPress.Invoke( );
		}
	}

	void OnMouseOver( )
	{
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			if ( aud != null )
				aud.PlayOneShot( clickSound );
			ClickEvent();

			if ( startTime )
				Time.timeScale = 1f;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
    {
		ClickEvent();
    }

	public void GoToScene( )
	{
		SceneManager.LoadScene( sceneName );
	}
}
