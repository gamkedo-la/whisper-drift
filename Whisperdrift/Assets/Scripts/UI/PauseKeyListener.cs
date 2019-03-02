using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseKeyListener : MonoBehaviour
{
	public GameObject pause;
	public Shooter shooter;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = pause.activeSelf ? 1f : 0f;
			shooter.enabled = pause.activeSelf;
			pause.SetActive(!pause.activeSelf);
		}
    }
}
