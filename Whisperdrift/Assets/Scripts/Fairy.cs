using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Fairy : MonoBehaviour
{

	[SerializeField] private static int totalFairiesFree = 0;
	[SerializeField] private int fairiesFree = 0;
	[SerializeField] private Light mainLight = null;
	[SerializeField] private SpriteRenderer mainSprite = null;
	[SerializeField] private ParticleSystem particleSys = null;
	private Transform fairyHome = null;
	[SerializeField] private float riseSpeed = 1f;
	[SerializeField] private float playSpeed = 8f;
	[SerializeField] private float fleeSpeed = 12f;
	[SerializeField] private float playRadius = 2f;
	[SerializeField] private float speedVariation = 0.25f;
	private enum behavior { rise, play, flee };
	private behavior currentBehavior = behavior.rise;
	private Vector3 destination;
	private Vector3 playOrigin;
	private int playCount = 6;
	private float distanceToDestination = Mathf.Infinity;
	private float speed = 0f;
	private const float RISE = 4f;
	private const float DISTANCE_THRESHOLD = 0.4f;

	void Start()
	{
		speedVariation = Random.Range(1f-speedVariation, 1f+speedVariation);
		fairyHome = GameObject.FindGameObjectWithTag("FairyHome").GetComponent<Transform>();
		Assert.IsNotNull(fairyHome);
		transform.SetParent(fairyHome);
		Score();
		StartRising();

		Assert.IsNotNull(mainLight);
		Assert.IsNotNull(mainSprite);
		Assert.IsNotNull(particleSys);
	}

	void Update()
	{
		distanceToDestination = Vector3.Distance(destination, transform.position);
		if (currentBehavior == behavior.rise && distanceToDestination <= DISTANCE_THRESHOLD) { StartPlaying(); }
		if (currentBehavior == behavior.play && distanceToDestination <= DISTANCE_THRESHOLD) { PlayNext(); }
		//if (currentBehavior == behavior.flee && distanceToDestination <= DISTANCE_THRESHOLD) { DisableFairyObject(); }

		transform.position += (destination - transform.position) * speed * Time.deltaTime;

	}

	

	private void Score() 
	{
		totalFairiesFree += 1;
		fairiesFree = totalFairiesFree;
	}

	private void StartRising()
	{
		destination = transform.position + (Vector3.up * RISE);
		speed = riseSpeed * speedVariation;
	}

	private void StartPlaying() 
	{
		currentBehavior = behavior.play;
		PlayNext();
		speed = playSpeed * speedVariation;
		playOrigin = transform.position;
	}

	private void StartFleeing() 
	{
		currentBehavior = behavior.flee;
		destination = fairyHome.position;
		speed = fleeSpeed * speedVariation;
	}

	private void PlayNext()
	{
		if (playCount <= 0) { StartFleeing(); return; }

		float rndX = Random.Range(-playRadius, playRadius);
		float rndY = Random.Range(-playRadius, playRadius);
		destination = new Vector3(playOrigin.x + rndX, playOrigin.y + rndY, playOrigin.z);
		playCount = playCount - 1;
	}

	private void DisableFairyObject()
	{
		this.gameObject.SetActive(false);
	}



}
