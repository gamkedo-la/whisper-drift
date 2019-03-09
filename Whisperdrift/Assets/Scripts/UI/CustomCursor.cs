using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
	[SerializeField] private GameObject cursorNormal = null;
	[SerializeField] private GameObject cursorPressed = null;
	[SerializeField] private Vector3 offset = Vector3.zero;

	private void Start( )
	{
		Cursor.visible = false;
		cursorNormal.SetActive( true );
		cursorPressed.SetActive( false );
	}

	private void Update( )
	{
		cursorNormal.transform.position = Input.mousePosition + offset;
		cursorPressed.transform.position = Input.mousePosition + offset;

		if ( Input.GetMouseButtonDown( 0 ) )
		{
			cursorNormal.SetActive( false );
			cursorPressed.SetActive( true );
		}

		if ( Input.GetMouseButtonUp( 0 ) )
		{
			cursorNormal.SetActive( true );
			cursorPressed.SetActive( false );
		}
	}
}