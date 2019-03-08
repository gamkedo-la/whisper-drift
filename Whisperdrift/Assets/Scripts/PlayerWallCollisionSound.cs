using UnityEngine;

public class PlayerWallCollisionSound : MonoBehaviour
{
	[SerializeField] private AnimationCurve hitVolCurve = new AnimationCurve();

    FMOD.Studio.EventInstance PlayerCollidesNonDamagingObjectSound;

    void Awake()
    {
        PlayerCollidesNonDamagingObjectSound = FMODUnity.RuntimeManager.CreateInstance("event:/player_collides_wall_or_platform");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.layer == 17 || collision.gameObject.CompareTag(Tags.Player) )
        {
			float hitVol = hitVolCurve.Evaluate( collision.relativeVelocity.magnitude );

			// Here we can control the volume
			Debug.Log( hitVol );
			PlayerCollidesNonDamagingObjectSound.setVolume( hitVol );

            PlayerCollidesNonDamagingObjectSound.start();
        }
    }
}
