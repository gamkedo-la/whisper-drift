using UnityEngine;
using UnityEngine.Assertions;

public class SlideDoor : MonoBehaviour
{
	[SerializeField] private Transform door1 = null;
	[SerializeField] private Transform door2 = null;
	[Space]
	[SerializeField] private Vector3 d1o;
	[SerializeField] private Vector3 d1d;
	[SerializeField] private Vector3 d2o;
	[SerializeField] private Vector3 d2d;
	[Space]
	[SerializeField] private float speed = 3;

	private float dir = -1;

	void Start ()
	{
		Assert.IsNotNull( door1 );
		Assert.IsNotNull( door2 );

		d1o = door1.transform.localPosition;
		d2o = door2.transform.localPosition;
	}

	void Update ()
	{
		Vector3 d1p = door1.transform.localPosition;
		Vector3 d2p = door2.transform.localPosition;

		d1p.y = Mathf.Clamp( d1p.y + dir * speed * Time.deltaTime, d1o.y, d1d.y );
		d2p.y = Mathf.Clamp( d2p.y + -dir * speed * Time.deltaTime, d2d.y, d2o.y );

		door1.transform.localPosition = d1p;
		door2.transform.localPosition = d2p;
	}

	public void DoorState ( bool isOpen )
	{
		dir = isOpen ? speed : -speed * 3;
	}
}
