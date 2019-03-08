using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseKeyListener : MonoBehaviour
{
	public GameObject pause;
	public GameObject pauseReminder;
	public Shooter shooter;

	bool paused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !paused )
		{
			Pause( );
		}
		else if ( Input.GetKeyDown( KeyCode.Escape ) && paused )
		{
			Resume( );
		}
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		shooter.enabled = false;
		pause.SetActive( true );
		pauseReminder.SetActive( false );
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		shooter.enabled = true;
		pause.SetActive( false );
		
		//OPINION. Once pressed paused, no need to show that anymore.
		
		//Uncomment code below to show pause reminder (Esc to Pause)...
		//...when player enters play mode back from pause menu
		
		//pauseReminder.SetActive( true );
	}
}
