using UnityEngine;
using UnityEngine.Assertions;

public class SlideDoor : MonoBehaviour
{
	[SerializeField] private Transform door1 = null;
	[SerializeField] private Transform door2 = null;
	[Space]
	[SerializeField] private Vector3 d1o = Vector3.zero;
	[SerializeField] private Vector3 d1d = Vector3.zero;
	[SerializeField] private Vector3 d2o = Vector3.zero;
	[SerializeField] private Vector3 d2d = Vector3.zero;
	[Space]
	[SerializeField] private float speed = 3;

	private float dir = -1;


    private FMOD.Studio.EventInstance slidingDoorSound;
    private bool slidingDoorSoundPlaying;
    private bool Released;
    FMOD.Studio.EventInstance PressureReleaseSound;
    FMOD.Studio.EventInstance DoorShutSound;
    private bool DoorShut;

    void Start ()
	{
		Assert.IsNotNull( door1 );
		Assert.IsNotNull( door2 );

		d1o = door1.transform.localPosition;
		d2o = door2.transform.localPosition;
       
        slidingDoorSound = FMODUnity.RuntimeManager.CreateInstance("event:/sliding_door_open_close");
        slidingDoorSoundPlaying = false;

        Released = false;
        PressureReleaseSound = FMODUnity.RuntimeManager.CreateInstance("event:/sliding_door_pressure_release_sound");

        DoorShutSound = FMODUnity.RuntimeManager.CreateInstance("event:/sliding_door_close");
        DoorShut = true;
    }

	void Update ()
	{
		Vector3 d1p = door1.transform.localPosition;
		Vector3 d2p = door2.transform.localPosition;

		d1p.y = Mathf.Clamp( d1p.y + dir * speed * Time.deltaTime, d1o.y, d1d.y );
		d2p.y = Mathf.Clamp( d2p.y + -dir * speed * Time.deltaTime, d2d.y, d2o.y );

		door1.transform.localPosition = d1p;
		door2.transform.localPosition = d2p;

        
        if ((door1.transform.localPosition.y > 2.7 && door1.transform.localPosition.y < 6.15) && !slidingDoorSoundPlaying)
        {
            if (!Released)
            {
                DoorShut = false;
                Released = true;
                PressureReleaseSound.start();
            }
            slidingDoorSound.start();
            slidingDoorSoundPlaying = true;
        }
        else if ((door1.transform.localPosition.y <= 2.7 || door1.transform.localPosition.y >= 6.15) && slidingDoorSoundPlaying )
        {
            if (Released)
            {
                Released = false;
            }

            slidingDoorSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // no effect?
            slidingDoorSoundPlaying = false;
        }

        if (door1.transform.localPosition.y <= 2.7  && !DoorShut)
        {
                DoorShutSound.start();
                DoorShut = true;
        }

    }

    public void DoorState ( bool isOpen )
	{
		dir = isOpen ? speed : -speed * 3;
	}
}
