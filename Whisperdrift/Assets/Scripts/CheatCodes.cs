using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
	void Update ()
	{
		if ( Input.GetKey( KeyCode.LeftShift ) || Input.GetKey( KeyCode.LeftShift ) )
		{
			if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
				SceneManager.LoadScene( "Level 01" );
			else if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
				SceneManager.LoadScene( "Level 02" );
			else if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
				SceneManager.LoadScene( "Level 03" );
			else if ( Input.GetKeyDown( KeyCode.Alpha4 ) )
				SceneManager.LoadScene( "Level 04" );
			else if ( Input.GetKeyDown( KeyCode.Alpha5 ) )
				SceneManager.LoadScene( "Level 05" );
			else if ( Input.GetKeyDown( KeyCode.Alpha6 ) )
				SceneManager.LoadScene( "Level 06" );
			else if ( Input.GetKeyDown( KeyCode.Alpha7 ) )
				SceneManager.LoadScene( "Level 07" );
		}
	}
}
