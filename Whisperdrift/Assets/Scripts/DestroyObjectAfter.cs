using UnityEngine;

public class DestroyObjectAfter : MonoBehaviour
{
	[SerializeField] private float destroyAfter = 10f;
	[SerializeField] private bool detachChildren = false;

	void Awake( )
	{
		Invoke( "DestroyMe", destroyAfter );
	}

	private void DestroyMe( )
	{
		if ( detachChildren )
			transform.DetachChildren( );

		Destroy( gameObject );
	}
}
