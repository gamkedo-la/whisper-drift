using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Whisp : MonoBehaviour
{

	[SerializeField] private static int totalWhispsFree = 0;
	[SerializeField] private int whispsFree = 0;
	[SerializeField] private Light mainLight = null;
	[SerializeField] private SpriteRenderer mainSprite = null;
	[SerializeField] private ParticleSystem particleSys = null;
	private Transform whispHome = null;
	[SerializeField] private float riseSpeed = 1f;
	[SerializeField] private float playSpeed = 8f;
	[SerializeField] private float fleeSpeed = 12f;
	[SerializeField] private float playRadius = 2f;
	[SerializeField] private float speedVariation = 0.25f;
	private Vector3 riseDirection = Vector3.up;
	[SerializeField] private float riseAmount = 4f;
	[SerializeField] private AudioClip freedomSound=null;
	[SerializeField] private AudioClip playSound=null;
	[SerializeField] private AudioClip fleeSound=null;
	private enum Behavior { Rise, Play, Flee };
	private Behavior currentBehavior = Behavior.Rise;
	private Vector3 destination;
	private Vector3 playOrigin;
	[SerializeField] private int playCount = 9;
	private float distanceToDestination = Mathf.Infinity;
	private float speed = 0f;
	private const float DISTANCE_THRESHOLD = 0.05f;
	private float whispVolume = 1f;


	void Start()
	{
		riseDirection = DetermineRiseDirection();
		speedVariation = Random.Range(1f-speedVariation, 1f+speedVariation);
		whispHome = GameObject.FindGameObjectWithTag("FairyHome").GetComponent<Transform>();
		Assert.IsNotNull(whispHome);
		transform.SetParent(whispHome);
		Score();
		StartRising();

		AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
		Assert.IsNotNull(audioSource);

		audioSource.volume = whispVolume * 0.5f;
		audioSource.Play();
		AudioSource.PlayClipAtPoint(freedomSound, transform.position, whispVolume);
		AudioSource.PlayClipAtPoint(fleeSound, transform.position, whispVolume);

		Assert.IsNotNull(mainLight);
		Assert.IsNotNull(mainSprite);
		Assert.IsNotNull(particleSys);
	}

	void Update()
	{
		distanceToDestination = Vector3.Distance(destination, transform.position);
		if (currentBehavior == Behavior.Flee && distanceToDestination <= DISTANCE_THRESHOLD) { DisableFairyObject(); }
		if (currentBehavior == Behavior.Rise && distanceToDestination <= DISTANCE_THRESHOLD) { StartPlaying(); }
		if (currentBehavior == Behavior.Play && distanceToDestination <= DISTANCE_THRESHOLD) { PlayNext(); }

		transform.position += (destination - transform.position).normalized * speed * Time.deltaTime;
	}

	private void Score() 
	{
		totalWhispsFree += 1;
		whispsFree = totalWhispsFree;
	}

	private Vector3 DetermineRiseDirection() 
	{
		Vector2 origin = transform.position;
		float dist = riseAmount + playRadius;
		RaycastHit2D rayHitDown = Physics2D.Raycast(origin, Vector2.down, dist, 1<<17);
		RaycastHit2D rayHitLeft = Physics2D.Raycast(origin, Vector2.left, dist, 1<<17);
		RaycastHit2D rayHitRight = Physics2D.Raycast(origin, Vector2.right, dist, 1<<17);

		if (!rayHitDown.collider) { return Vector3.down; }
		if (!rayHitLeft.collider) { return Vector3.left; }
		if (!rayHitRight.collider) { return Vector3.right; }
		return Vector3.up;
	}

	private void StartRising()
	{
		destination = transform.position + (riseDirection * riseAmount);
		speed = riseSpeed * speedVariation;
	}

	private void StartPlaying() 
	{
		currentBehavior = Behavior.Play;
		PlayNext();
		speed = playSpeed * speedVariation;
		playOrigin = transform.position;
	}

	private void StartFleeing() 
	{
		AudioSource.PlayClipAtPoint(fleeSound, transform.position, whispVolume);
		currentBehavior = Behavior.Flee;
		destination = whispHome.position;
		speed = fleeSpeed * speedVariation;
	}

	private void PlayNext()
	{
		if (playCount <= 0) { StartFleeing(); return; }

		AudioSource.PlayClipAtPoint(playSound, transform.position, whispVolume);
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
