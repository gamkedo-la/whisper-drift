using UnityEngine;

public class PlayerSpawnSound : MonoBehaviour
{
    FMOD.Studio.EventInstance player_spawn_sound;

	void Awake()
    {
        player_spawn_sound = FMODUnity.RuntimeManager.CreateInstance("event:/player_spawn_sound");
    }

    void Start()
    {
        player_spawn_sound.start();
    }
}
