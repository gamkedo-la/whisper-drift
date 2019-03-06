using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
	[SerializeField] private Image cursor = null;
	[SerializeField] private Color clickColor = Color.white;
	[SerializeField] private Color normalColor= Color.white;
	[SerializeField] private Vector3 offset = Vector3.zero;

	private void Start( )
	{
		Cursor.visible = false;

		if ( cursor == null )
			cursor = GetComponent<Image>( );
	}

	private void Update( )
	{
		transform.position = Input.mousePosition + offset;

		if ( Input.GetMouseButtonDown( 0 ) )
			cursor.color = clickColor;

		if ( Input.GetMouseButtonUp( 0 ) )
			cursor.color = normalColor;
	}
}