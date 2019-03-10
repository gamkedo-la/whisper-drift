using UnityEngine;

public class PlayerWallCollisionSound : MonoBehaviour
{
	[SerializeField] private GameObject hitEffect = null;
	[SerializeField] private float hitEffectMinMag = 8f;
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

			PlayerCollidesNonDamagingObjectSound.setVolume( hitVol ); // Here we can control the volume
			PlayerCollidesNonDamagingObjectSound.start();

			if ( collision.relativeVelocity.magnitude  >= hitEffectMinMag )
			{
				ShakeEffect.Instance.DoVerySmallShake( );
				Instantiate( hitEffect, transform.position/*collision.contacts[0].point*/, Quaternion.identity );
			}
        }
    }
}
