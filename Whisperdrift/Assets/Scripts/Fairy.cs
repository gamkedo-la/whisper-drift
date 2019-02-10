using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Fairy : MonoBehaviour
{

	[SerializeField] private static int totalFariesFree = 0;
	[SerializeField] private Light mainLight = null;
	[SerializeField] private SpriteRenderer mainSprite = null;
	[SerializeField] private ParticleSystem particles = null;
	private Transform fairyHome = null;
	[SerializeField] private float riseSpeed = 1f;
	[SerializeField] private float playSpeed = 8f;
	[SerializeField] private float fleeSpeed = 12f;
	private float speed = 0f;
	private const float PLAY_AREA = 1f;
	private const float RISE = 4f;
	private Vector3 destination;
	private int playCount = 6;
	private enum behavior { rise, play, flee};
	private behavior currentBehavior = behavior.rise;
	private float distanceToDestination = Mathf.Infinity;
	private const float DISTANCE_THRESHOLD = 0.2f;

	void Start()
	{
		fairyHome = GameObject.FindGameObjectWithTag("FairyHome").GetComponent<Transform>();
		Assert.IsNotNull(fairyHome);
		transform.SetParent(fairyHome);
		Score();
		StartRising();

		Assert.IsNotNull(mainLight);
		Assert.IsNotNull(mainSprite);
		Assert.IsNotNull(particles);
	}

	void Update()
	{
		distanceToDestination = Vector3.Distance(destination, transform.position);
		if (currentBehavior == behavior.rise && distanceToDestination <= DISTANCE_THRESHOLD) { StartPlaying(); }
		if (currentBehavior == behavior.play && distanceToDestination <= DISTANCE_THRESHOLD) { PlayNext(); }
		if (currentBehavior == behavior.flee && distanceToDestination <= DISTANCE_THRESHOLD) { DisableFairyObject(); }

		transform.position += (destination - transform.position) * speed * Time.deltaTime;

	}

	

	private void Score() 
	{
		totalFariesFree += 1;
	}

	private void StartRising()
	{
		destination = transform.position + (Vector3.up * RISE);
		speed = riseSpeed;
	}

	private void StartPlaying() 
	{
		currentBehavior = behavior.play;
		PlayNext();
		speed = playSpeed;
	}

	private void StartFleeing() 
	{
		currentBehavior = behavior.flee;
		destination = fairyHome.position;
		speed = fleeSpeed;
	}

	private void PlayNext()
	{
		if (playCount <= 0) { StartFleeing(); return; }

		float rndX = Random.Range(-PLAY_AREA, PLAY_AREA);
		float rndY = Random.Range(-PLAY_AREA, PLAY_AREA);
		destination = new Vector3(transform.position.x + rndX, transform.position.y + rndY, transform.position.z);
		playCount = playCount - 1;
	}

	private void DisableFairyObject()
	{
		this.gameObject.SetActive(false);
	}



}
