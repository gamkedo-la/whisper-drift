using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	public Vector3 newScale = new Vector3(1.2f, 1.2f, 1.2f);
	
	public bool usingWorldSpaceCanvas = true;
	
	public AudioClip hoverSound;
	
	private AudioSource aud = null;
	
	private Vector3 previousScale = new Vector3(1f, 1f, 1f);
	
	private bool doScale = false;
	
	void Start () {
		aud = GetComponent<AudioSource>();
		if(aud == null)
			aud = FindObjectOfType<AudioSource>();
		
		previousScale = transform.localScale;
	}
	
	void Update () {
		transform.localScale = doScale ? newScale : previousScale;
		doScale = usingWorldSpaceCanvas ? false : doScale;
	}
	
	void OnMouseOver() {
		if(enabled && transform.localScale != newScale && aud != null)
			aud.PlayOneShot(hoverSound);
		doScale = true;
	}
	
	public void OnPointerEnter(PointerEventData eventData) {
		if(enabled && transform.localScale != newScale && aud != null)
			aud.PlayOneShot(hoverSound);
		doScale = true;
	}
	
	public void OnPointerExit(PointerEventData eventData)
    {
		doScale = false;
    }
	
	public void ScaleBackToNormal()
	{
		transform.localScale = previousScale;
	}
}
