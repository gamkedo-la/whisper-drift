using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeButton : MonoBehaviour, IPointerClickHandler
{
    void Update()
    {
        
    }
	
	public void OnPointerClick(PointerEventData eventData)
    {
		Time.timeScale = 1f;
		transform.parent.gameObject.SetActive(false);
    }
}
