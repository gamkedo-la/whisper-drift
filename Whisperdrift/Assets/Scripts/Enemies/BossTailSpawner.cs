using UnityEngine;
using UnityEngine.Assertions;

public class BossTailSpawner : MonoBehaviour
{
	[SerializeField] private GameObject tail = null;
	[SerializeField] private float spawnDelay = 1f;
	[SerializeField] private float disapearTime = 6f;

	private float nextSpawnIn = 0;
	private float disapearTimeCurrent = 0;

	void Start ()
	{
		Assert.IsNotNull( tail );

		nextSpawnIn = spawnDelay;
	}

	void Update ()
	{
		nextSpawnIn -= Time.deltaTime;

		if (nextSpawnIn <= 0)
		{
			nextSpawnIn = spawnDelay;
			GameObject f = Instantiate( tail, transform.position, Quaternion.identity );
			f.GetComponent<BossTail>( ).SetDisapearTime( disapearTime );
		}
	}
}
