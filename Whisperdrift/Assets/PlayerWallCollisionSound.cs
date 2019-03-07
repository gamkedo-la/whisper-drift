using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCollisionSound : MonoBehaviour
{

    FMOD.Studio.EventInstance PlayerCollidesNonDamagingObjectSound;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerCollidesNonDamagingObjectSound = FMODUnity.RuntimeManager.CreateInstance("event:/player_collides_wall_or_platform");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.layer == 17 || collision.gameObject.CompareTag(Tags.Player) )
        {
            PlayerCollidesNonDamagingObjectSound.start();
        }
    }
}
