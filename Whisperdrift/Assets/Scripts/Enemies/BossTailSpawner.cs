using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BossTailSpawner : MonoBehaviour
{
	public static BossTailSpawner Instance { get; private set; }
	public List<GameObject> Tails;

	[SerializeField] private GameObject crystal = null;
	[SerializeField] private GameObject explosion = null;
	[SerializeField] private GameObject tail = null;
	[SerializeField] private float spawnDelay = 1f;
	[SerializeField] private float disapearTime = 6f;
	[SerializeField] private int disapearCount = 10;
	[SerializeField] private AnimationCurve disapearCurve = new AnimationCurve();

	private float nextSpawnIn = 0;
	private float disapearTimeCurrent = 0;
	private int disapearCountCurrent = 0;

	private void Awake( )
	{
		Tails = new List<GameObject>( );

		if ( Instance != null && Instance != this )
			Destroy( gameObject );
		else
			Instance = this;
	}

	private void OnDestroy( ) { if ( this == Instance ) { Instance = null; } }

	public void Kill( )
	{
		Instantiate( crystal, transform.position, Quaternion.identity );
		GameObject e = Instantiate( explosion, transform.position, Quaternion.identity );
		e.transform.localScale = Vector3.one * 2;

		for ( int i = Tails.Count -1; i >= 0; i-- )
		{
			Tails[i].GetComponent<BossTail>( ).DestroyMe( 0.1f * ( Tails.Count - i ) );
		}
	}

	public void GotHit()
	{
		disapearCountCurrent--;
		disapearTimeCurrent = disapearTime * disapearCurve.Evaluate( 1f - (float)disapearCountCurrent / disapearCount );
	}

	void Start ()
	{
		Assert.IsNotNull( tail );

		nextSpawnIn = spawnDelay;
		disapearTimeCurrent = disapearTime;
		disapearCountCurrent = disapearCount;
	}

	void Update ()
	{
		nextSpawnIn -= Time.deltaTime;

		if (nextSpawnIn <= 0)
		{
			nextSpawnIn = spawnDelay;
			GameObject f = Instantiate( tail, transform.position, Quaternion.identity );
			f.GetComponent<BossTail>( ).SetDisapearTime( disapearTimeCurrent );
		}
	}
}
