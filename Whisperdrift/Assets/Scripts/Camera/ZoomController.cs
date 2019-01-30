using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
	private Camera cam = null;
	private float zoomLevel = 5f;
	private float tempZoomLevel = 5f;
	private const float ZOOMLEVEL_CHANGE_SPEED = 0.5f;
	private const float MIN_MOVEMENT_FOR_ZOOM_IN = .1f;
	private const float MIN_MOVEMENT_FOR_ZOOM_OUT = .1f;
	private const float MIN_ZOOM_LEVEL = 5f;
	private const float MAX_ZOOM_LEVEL = 14f;
	private const float ZOOM_FACTOR = 1f;



	void Start()
    {
		cam = Camera.main;
		cam.orthographicSize = zoomLevel;
		zoomLevel = MIN_ZOOM_LEVEL;
		tempZoomLevel = MIN_ZOOM_LEVEL;
		SetCameraSize(tempZoomLevel);
    }

	private void FixedUpdate()
	{
		if (zoomLevel + MIN_MOVEMENT_FOR_ZOOM_OUT > tempZoomLevel) 
		{
			tempZoomLevel = tempZoomLevel + (ZOOMLEVEL_CHANGE_SPEED * Time.deltaTime);
			if (tempZoomLevel > zoomLevel) { tempZoomLevel = zoomLevel; }
			SetCameraSize(tempZoomLevel);
		}
		if (zoomLevel - MIN_MOVEMENT_FOR_ZOOM_IN < tempZoomLevel)
		{
			tempZoomLevel = tempZoomLevel - (ZOOMLEVEL_CHANGE_SPEED * Time.deltaTime);
			if (tempZoomLevel < zoomLevel) { tempZoomLevel = zoomLevel; }
			SetCameraSize(tempZoomLevel);
		}

	}

	public void Zoom (float amount)
	{
		zoomLevel = Mathf.Clamp(zoomLevel + (amount * ZOOM_FACTOR), MIN_ZOOM_LEVEL, MAX_ZOOM_LEVEL);
	}

	private void SetCameraSize(float newSize) 
	{
		cam.orthographicSize = newSize;
	}
}
