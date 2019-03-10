using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playDoorCollisionSound : MonoBehaviour
{
    private FMOD.Studio.EventInstance swingingDoorCollisionSound;
    
    void Start()
    {
        swingingDoorCollisionSound = FMODUnity.RuntimeManager.CreateInstance("event:/swinging_door_collision");
        Debug.Log(swingingDoorCollisionSound);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        swingingDoorCollisionSound.start();
        Debug.Log("OnCollisionEnter2D");
    }
}
