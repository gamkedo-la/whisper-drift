using UnityEngine;

public class FMODPlaySound : MonoBehaviour
{
	[FMODUnity.EventRef] public string Event;
	public float Volume = 1f;

	private FMOD.Studio.EventInstance sound;

	private void Start( )
	{
		sound = FMODUnity.RuntimeManager.CreateInstance( Event );
		sound.setVolume( Volume );
		sound.start( );
	}
}
