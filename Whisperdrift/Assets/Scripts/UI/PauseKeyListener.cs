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
		paused = true;
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		shooter.enabled = true;
		pause.SetActive( false );
		pauseReminder.SetActive( true );
		paused = false;
	}
}
