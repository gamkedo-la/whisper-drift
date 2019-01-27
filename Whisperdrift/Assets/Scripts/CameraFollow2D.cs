using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float smoothSpeed = 0.2f;
	[SerializeField] private Vector3 offset = new Vector3( 0, 0, -10 );

    void Start( )
    {
        Assert.IsNotNull( target );
    }

	void FixedUpdate ( )
    {
        float desireX = Mathf.SmoothStep( transform.position.x, target.position.x + offset.x, smoothSpeed );
        float desireY = Mathf.SmoothStep( transform.position.y, target.position.y + offset.y, smoothSpeed) ;
        transform.position = new Vector3( desireX,desireY, offset.z );
	}
}
