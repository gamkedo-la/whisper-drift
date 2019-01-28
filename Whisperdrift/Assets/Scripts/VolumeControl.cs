using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
	[SerializeField] private AudioMixer audioMixer = null;
	[SerializeField] private Slider volumeSlider = null;
	[SerializeField] private string exposedPropertyName = "Volume";

	void Start( )
	{
		Assert.IsNotNull( audioMixer );
		Assert.IsNotNull( volumeSlider );

		volumeSlider.value = PlayerPrefs.GetFloat( exposedPropertyName, 1f );
	}

	public void SetVolume( float value )
	{
		audioMixer.SetFloat( exposedPropertyName, Mathf.Log10( value ) * 20 );
		PlayerPrefs.SetFloat( exposedPropertyName, value );
	}
}
