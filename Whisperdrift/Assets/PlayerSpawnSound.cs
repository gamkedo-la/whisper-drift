using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSound : MonoBehaviour
{
    FMOD.Studio.EventInstance player_spawn_sound;
    // Start is called before the first frame update

    private void Awake()
    {
        player_spawn_sound = FMODUnity.RuntimeManager.CreateInstance("event:/player_spawn_sound");
    }
    void Start()
    {
        player_spawn_sound.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
