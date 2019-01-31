using UnityEngine;

public class ZoomController : MonoBehaviour
{
	[SerializeField] private float zoomSpeed = 0.5f;
	[SerializeField] private bool showMaxVision = false;
	[SerializeField] private float minSpeedBeforeZoom = 2.0f;

	private Camera cam = null;
	private float zoomLevel = 4f;
	private float tempZoomLevel = 4f;

	private const float MIN_ZOOM_LEVEL = 5f;
	private const float MAX_ZOOM_LEVEL = 9f;
	private const float ZOOM_FACTOR = 2f;

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
		if (zoomLevel > tempZoomLevel)
		{
			tempZoomLevel = tempZoomLevel + (zoomSpeed * Time.deltaTime);
		}
		else if (tempZoomLevel > zoomLevel)
		{
			tempZoomLevel = tempZoomLevel - (zoomSpeed * Time.deltaTime);
		}
		tempZoomLevel = Mathf.Clamp(tempZoomLevel, MIN_ZOOM_LEVEL, MAX_ZOOM_LEVEL);
	}

	private void OnDrawGizmos()
	{
		if (showMaxVision)
		{
			float drawMaxY = MAX_ZOOM_LEVEL*2;
			float drawMinY = MIN_ZOOM_LEVEL*2;
			float drawMaxX = drawMaxY * Camera.main.aspect;
			float drawMinX = drawMinY * Camera.main.aspect;
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireCube(transform.position, new Vector3(drawMaxX, drawMaxY, 0f));
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(transform.position, new Vector3(drawMinX, drawMinY, 0f));
			Gizmos.color = Color.white;
		}
	}

	public void LateUpdate()
	{
		SetCameraSize(tempZoomLevel);
	}

	public void Zoom (float speed)
	{
		zoomLevel = Mathf.Clamp(MIN_ZOOM_LEVEL + (speed * ZOOM_FACTOR) - minSpeedBeforeZoom, MIN_ZOOM_LEVEL, MAX_ZOOM_LEVEL);
	}

	private void SetCameraSize(float newSize)
	{
		cam.orthographicSize = newSize;
	}
}
