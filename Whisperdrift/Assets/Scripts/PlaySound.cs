using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent( typeof( AudioSource ) )]
public class PlaySound : MonoBehaviour
{
	[Header("External objects")]
	[SerializeField] private AudioClip[] sounds = null;

	[Header("Sound parameters")]
	[SerializeField] private float minPitch = 0.8f;
	[SerializeField] private float maxPitch = 1.2f;
	[SerializeField] private float minVolume = 0.9f;
	[SerializeField] private float maxVolume = 1.0f;

	private AudioSource audioSource;

	void Start( )
	{
		audioSource = GetComponent<AudioSource>( );

		Assert.IsNotNull( audioSource );
		Assert.IsNotNull( sounds );
		Assert.AreNotEqual( sounds.Length, 0, "You have to add at least one sound clip to " + name );

		Play( );
	}

	private void Play( )
	{
		audioSource.clip = sounds[Random.Range( 0, sounds.Length )];
		audioSource.pitch = Random.Range( minPitch, maxPitch );
		audioSource.volume = Random.Range( minVolume, maxVolume );
		audioSource.Play( );
	}
}
