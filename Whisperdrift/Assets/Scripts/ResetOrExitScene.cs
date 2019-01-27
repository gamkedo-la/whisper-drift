using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetOrExitScene : MonoBehaviour
{
	[SerializeField] private bool allowExit = true;

	void Update( )
	{
		if ( Input.GetKeyDown( KeyCode.R ) )
			SceneManager.LoadScene( gameObject.scene.name );

		if ( allowExit && Input.GetKeyDown( KeyCode.Escape ) )
			Application.Quit( );

#if UNITY_EDITOR
		//if ( allowExit && Input.GetKeyDown( KeyCode.Escape ) )
		//UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
