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
	[SerializeField] private string nextScene = "Main";
	[SerializeField] private float levelChangeDelay = 1.0f;
	[SerializeField] private UnityEventFloat onLevelProgress = null;

	private int activeRingGates = 0;
	private List<RingGate> ringGates;
	private RingGateExit ringGateExit;

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
			label.text = "Exit active";
		}

		onLevelProgress.Invoke( (float)activeRingGates / ringGates.Count );
	}

	public void ExitActivated( )
	{
		Invoke( "ChangeLevel", levelChangeDelay );
	}

	private void UpdateLabel( )
	{
		label.text = $"Rings active: {activeRingGates}/{ringGates.Count}";
	}

	private void ChangeLevel( )
	{
		SceneManager.LoadScene( nextScene );
	}
}
