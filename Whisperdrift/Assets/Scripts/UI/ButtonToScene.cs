using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToScene : MonoBehaviour {
	
	public string sceneName = "null";
	
	public bool startTime = false;
	
	public AudioClip clickSound;
	
	private AudioSource aud = null;

	void Start () {
		aud = GetComponent<AudioSource>();
		if(aud == null)
			aud = FindObjectOfType<AudioSource>();
	}
	
	void Update () {
		
	}
	
	void OnMouseOver() {
		if(Input.GetMouseButtonDown(0))
		{
			if(aud != null)
			{
				aud.PlayOneShot(clickSound);
			}

   		   	if (sceneName == "Quit")
				Application.Quit();
			else if (sceneName == "Reset")
				SceneManager.LoadScene(gameObject.scene.name);
     		else
				SceneManager.LoadScene(sceneName);
			
			if(startTime) Time.timeScale = 1f;
		}
		
	}
}
