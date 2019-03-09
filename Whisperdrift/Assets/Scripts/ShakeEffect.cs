using UnityEngine;
using UnityEngine.Assertions;

public class ShakeEffect : MonoBehaviour
{
	public static ShakeEffect Instance { get; private set; }

	[SerializeField] private Animator animator = null;

	private void Awake( )
	{
		if ( Instance != null && Instance != this )
			Destroy( gameObject );
		else
			Instance = this;
	}

	private void OnDestroy( ) { if ( this == Instance ) { Instance = null; } }

	void Start ()
	{
		Assert.IsNotNull( animator );
	}

	public void DoVerySmallShake( )
	{
		animator.SetTrigger( "Very Small Shake" );
	}

	public void DoSmallShake( )
	{
		animator.SetTrigger( "Small Shake" );
	}

	public void DoBigShake ()
	{
		animator.SetTrigger( "Big Shake" );
	}
}
