using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BossTailSpawner : MonoBehaviour
{
	public static BossTailSpawner Instance { get; private set; }
	public List<GameObject> Tails;

	[SerializeField] private HP hp = null;
	[SerializeField] private GameObject explosion = null;
	[SerializeField] private GameObject tail = null;
	[SerializeField] private float spawnDelay = 1f;
	[SerializeField] private float disapearTime = 6f;
	[SerializeField] private int disapearCount = 10;
	[SerializeField] private AnimationCurve disapearCurve = new AnimationCurve();
	[FMODUnity.EventRef, SerializeField] private string soundEvent = null;

	private float nextSpawnIn = 0;
	private float disapearTimeCurrent = 0;
	private int disapearCountCurrent = 0;
	private FMOD.Studio.EventInstance sound;

	private void Awake( )
	{
		Tails = new List<GameObject>( );

		if ( Instance != null && Instance != this )
			Destroy( gameObject );
		else
			Instance = this;
	}

	private void OnDestroy( ) { if ( this == Instance ) { Instance = null; } }

	void Start ()
	{
		Assert.IsNotNull( hp );
		Assert.IsNotNull( tail );

		nextSpawnIn = spawnDelay;
		disapearTimeCurrent = disapearTime;
		disapearCountCurrent = disapearCount;

		sound = FMODUnity.RuntimeManager.CreateInstance( soundEvent );

		//Debug.Log( "D: " + disapearTimeCurrent + " %: " + disapearCurve.Evaluate( 1f - 1f - hp.CurrentHP / hp.MaxHP ) );
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

	public void GotHit( )
	{
		disapearTimeCurrent = disapearTime * disapearCurve.Evaluate( 1f - hp.CurrentHP / hp.MaxHP );
		//Debug.Log( "D: " + disapearTimeCurrent + " %: " + disapearCurve.Evaluate( 1f - hp.CurrentHP / hp.MaxHP ));
	}

	public void Kill( )
	{
		LevelManger.Instance.ShowCrystal( transform.position );

		sound.start( );

		GameObject e = Instantiate( explosion, transform.position, Quaternion.identity );
		e.transform.localScale = Vector3.one * 2;

		for ( int i = Tails.Count -1; i >= 0; i-- )
		{
			Tails[i].GetComponent<BossTail>( ).DestroyMe( 0.1f * ( Tails.Count - i ) );
		}

		FindObjectOfType<MusicEnd>( ).enabled = true;
	}
}
