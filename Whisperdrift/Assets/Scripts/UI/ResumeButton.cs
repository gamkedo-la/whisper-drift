using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeButton : MonoBehaviour, IPointerClickHandler
{
	public PauseKeyListener pause = null;

	public void OnPointerClick(PointerEventData eventData)
    {
		pause.Resume( );
    }
}
