using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckIfFMODBanksLoaded : MonoBehaviour
{
	void Update ()
	{
		if ( FMODUnity.RuntimeManager.HasBankLoaded( "Master Bank" ) &&
			 FMODUnity.RuntimeManager.HasBankLoaded( "Master Bank.strings" ) )
		{
			Debug.Log( "All Banks Loaded" );
			SceneManager.LoadScene( "Main Menu", LoadSceneMode.Single );
		}
	}
}
