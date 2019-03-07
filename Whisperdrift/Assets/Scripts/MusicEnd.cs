using UnityEngine;
using UnityEngine.Assertions;

public class MusicEnd : MonoBehaviour
{
	[SerializeField] private AudioSource music = null;
	[SerializeField] private float maxVol = 1f;
	[SerializeField] private float time = 1f;

	private float currentTime = 0;

	void Start ()
	{
		Assert.IsNotNull( music );

		currentTime = time;
	}

	void Update ()
	{
		currentTime -= Time.deltaTime;

		if (currentTime < 0)
			Destroy( gameObject );
		else
			music.volume = maxVol * ( currentTime / time );
	}
}
