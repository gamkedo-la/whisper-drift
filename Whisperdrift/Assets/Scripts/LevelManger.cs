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

	private Vector2 crystalPos;
	private int activeRingGates = 0;
	private List<RingGate> ringGates;
	private RingGateExit ringGateExit = null;

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
		onLevelEnd.Invoke( );
	}

	public void ChangeLevel( )
	{
		SceneManager.LoadScene( nextScene );
	}
}
