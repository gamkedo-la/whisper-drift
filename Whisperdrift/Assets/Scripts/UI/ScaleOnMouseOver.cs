using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnMouseOver : MonoBehaviour {
	
	public Vector3 newScale = new Vector3(1.2f, 1.2f, 1.2f);
	
	public AudioClip hoverSound;
	
	private AudioSource aud = null;
	
	private Vector3 previousScale = new Vector3(1f, 1f, 1f);
	
	private bool doScale = false;
	
	//FIX FOR SCALING TOWARDS DOWN ISSUE
	private Vector3 previousPosition;
	
	void Start () {
		aud = GetComponent<AudioSource>();
		if(aud == null)
			aud = FindObjectOfType<AudioSource>();
		
		previousScale = transform.localScale;
		
		//FIX FOR SCALING TOWARDS DOWN ISSUE
		previousPosition = transform.position;
	}
	
	void Update () {
		if(doScale)
		{
			transform.localScale = newScale;
			
			//FIX FOR SCALING TOWARDS DOWN ISSUE
			transform.position = previousPosition + new Vector3(0f, 0.3f, 0f);
		}
		else
		{
			transform.localScale = previousScale;
			
			//FIX FOR SCALING TOWARDS DOWN ISSUE
			transform.position = previousPosition;
		}
		
		doScale = false;
	}
	
	void OnMouseOver() {
		if(enabled && transform.localScale != newScale && aud != null)// && TogglesValues.sound)
		{
			aud.PlayOneShot(hoverSound);
		}
		doScale = true;
	}
	
	public void ScaleBackToNormal()
	{
		transform.localScale = previousScale;
	}
}
