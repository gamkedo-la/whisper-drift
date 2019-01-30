using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleController : MonoBehaviour
{
    private const float WOBBLE_INTERVAL = 6f;
	private const float WOBBLE_SPEED = 1f;
	private const float MAX_WOBBLE_AMT = 10f;
	private float goalWobble = 0f;
	private float currentWobble = 0f;
	private float timer = 4f;

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		timer = timer + Time.deltaTime;
		if (timer > WOBBLE_INTERVAL) { SetNewWobble(); }

		if (currentWobble < goalWobble)
		{
			currentWobble = currentWobble + (WOBBLE_SPEED * Time.deltaTime);
			if (currentWobble > goalWobble) { currentWobble = goalWobble; }
			transform.localRotation = Quaternion.Euler (transform.localRotation.x, transform.localRotation.y, currentWobble);
		}
		else if (currentWobble > goalWobble) 
		{
			currentWobble = currentWobble - (WOBBLE_SPEED * Time.deltaTime);
			if (currentWobble < goalWobble) { currentWobble = goalWobble; }
			transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, currentWobble);
		}
		
    }

	private void SetNewWobble() 
	{
		goalWobble = Random.Range(-MAX_WOBBLE_AMT, MAX_WOBBLE_AMT);
		timer = 0f;
	}
}
