using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
	[SerializeField] private AudioMixer audioMixer = null;
	[SerializeField] private Slider volumeSlider = null;
	[SerializeField] private string exposedPropertyName = "Volume";
	
    FMOD.Studio.Bus SoundEffectsBus;

	void Start( )
	{
		Assert.IsNotNull( audioMixer );
		Assert.IsNotNull( volumeSlider );

		volumeSlider.value = PlayerPrefs.GetFloat( exposedPropertyName, 1f );

        SoundEffectsBus = FMODUnity.RuntimeManager.GetBus("bus:/sound_effects");
	}

	public void SetVolume( float value )
	{
		audioMixer.SetFloat( exposedPropertyName, Mathf.Log10( value ) * 20 );
		PlayerPrefs.SetFloat( exposedPropertyName, value );
        SoundEffectsBus.setVolume(value);
	}
}
