using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UnityEventFloat : UnityEvent<float> { }

public class LevelManger : MonoBehaviour
{
	public static LevelManger Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI label = null;
	[SerializeField] private TextMeshProUGUI exit = null;
	[SerializeField] private string nextScene = "Main";
	[SerializeField] private float levelChangeDelay = 1.0f;
	[SerializeField] private UnityEventFloat onLevelProgress = null;
	[SerializeField] private UnityEvent onLevelEnd = null;
	[Header("Boss Level")]
	[SerializeField] private GameObject music = null;
	[SerializeField] private GameObject endEffect = null;
	[SerializeField] private GameObject crystal = null;
	[SerializeField] private GameObject crystalEffect = null;
	[SerializeField] private GameObject rescudeWhisp = null;
	[SerializeField] private Vector2 center = new Vector2(-3f, 34);
	[SerializeField] private float radius = 23f;
	[SerializeField] private bool isBossLevel = false;

	private Vector2 crystalPos;
	private int activeRingGates = 0;
	private List<RingGate> ringGates;
	private RingGateExit ringGateExit = null;
	private int faeries = 0;
	private int faeriesAvailable = 0;

	private void Awake( )
	{
		ringGates = new List<RingGate>( );

		if ( Instance != null && Instance != this )
			Destroy( gameObject );
		else
			Instance = this;
	}

	private void OnDestroy( ) { if ( this == Instance ) { Instance = null; } }

	void Start( )
	{
		Assert.IsNotNull( label );
		Assert.IsNotNull( exit );

		if ( isBossLevel )
		{
			faeriesAvailable = 0;
			faeriesAvailable += PlayerPrefs.GetInt( "Level 01", 0 );
			faeriesAvailable += PlayerPrefs.GetInt( "Level 02", 0 );
			faeriesAvailable += PlayerPrefs.GetInt( "Level 03", 0 );
			faeriesAvailable += PlayerPrefs.GetInt( "Level 04", 0 );
			faeriesAvailable += PlayerPrefs.GetInt( "Level 05", 0 );
			faeriesAvailable += PlayerPrefs.GetInt( "Level 06", 0 );

			Debug.Log( "Faeries available: " + faeriesAvailable );

			for ( int i = 0; i < faeriesAvailable; i++ )
			{
				InstNewWhisp( );
			}
		}
	}

	private void InstNewWhisp()
	{
		Instantiate( rescudeWhisp, NewDestination( ), Quaternion.identity );
	}

	private Vector2 NewDestination( )
	{
		return new Vector2
		(
			Random.Range( center.x - radius, center.x + radius ),
			Random.Range( center.y - radius, center.y + radius )
		);
	}

	public void Faerie( )
	{
		faeries++;
	}

	public void AddRingGate( RingGate ringGate )
	{
		ringGates.Add( ringGate );
		UpdateLabel( );
	}

	public void AddRingGateExit( RingGateExit ringGateExit )
	{
		this.ringGateExit = ringGateExit;
	}

	public void RingGateActiveted( RingGate ringGate )
	{
		activeRingGates++;
		UpdateLabel( );

		if ( activeRingGates == ringGates.Count )
		{
			ringGateExit.PowerUp( );
			label.transform.parent.gameObject.SetActive( false );
			exit.gameObject.SetActive( true );
		}

		onLevelProgress.Invoke( (float)activeRingGates / ringGates.Count );
	}

	public void ExitActivated( )
	{
        Invoke( "LevelEnd", levelChangeDelay );
	}

	public void ShowCrystal( Vector2 position )
	{
		crystalPos = position;

		Invoke( "SpawnCrystalEffect", 2f );
		Invoke( "SpawnCrystal", 2.5f );
	}

	public void EndBossLevel( )
	{
		Invoke( "PlayEndMusic", 0.2f );
		Invoke( "SpiritAway", 0.5f );
		Invoke( "TheEnd", 1f );
	}

	private void SpawnCrystalEffect()
	{
		Instantiate( crystalEffect, crystalPos, Quaternion.identity );
	}

	private void SpawnCrystal()
	{
		Instantiate( crystal, crystalPos, Quaternion.identity );
	}

	private void PlayEndMusic( )
	{
		Instantiate( music, transform.position, Quaternion.identity );
	}

	private void SpiritAway( )
	{
		GameObject player = GameObject.FindGameObjectWithTag( "Player" );
		Instantiate( endEffect, player.transform.position, Quaternion.identity );
		Destroy( player );
	}

	private void TheEnd( )
	{
		GameObject.Find( "Screen Transition Out of Level" ).GetComponent<ScreenTransition>( ).StartTransition( );
	}

	private void UpdateLabel( )
	{
		label.text = $"{activeRingGates}/{ringGates.Count}";
	}

	private void LevelEnd( )
	{
		if ( PlayerPrefs.GetInt( gameObject.scene.name ) < faeries )
		{
			PlayerPrefs.SetInt( gameObject.scene.name, faeries );
			Debug.Log( "Saved: " + faeries );
		}
		onLevelEnd.Invoke( );
	}

	public void ChangeLevel( )
	{
		SceneManager.LoadScene( nextScene );
	}
}
