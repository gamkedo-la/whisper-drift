using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Slider freezeBar = null;
	[SerializeField] private float shotForce = 50f;
	[SerializeField] private float freezeDecline = 50f;
	[SerializeField] private float freezeRegen = 10f;
	[SerializeField] private float freezeMax = 100f;

	private Rigidbody2D rb;
	private ZoomController zoomController;
	private bool isFrozen = false;
	private float freezeAvailable = 0;
	private bool canFreeze = true;
	private Vector2 oldVelocity = Vector2.zero;
	private Vector3 freezeAccumulatedForce = Vector3.zero;
    private FMOD.Studio.EventInstance playerFreezeSound;
    private FMOD.Studio.EventInstance cantFreezeSound;
    private bool alreadyFrozen;
    private bool alreadyCantFreeze;
    

    void Start( )
	{
		rb = GetComponent<Rigidbody2D>( );
		Assert.IsNotNull( rb );
		Assert.IsNotNull( freezeBar );

		zoomController = FindObjectOfType<ZoomController>();
		Assert.IsNotNull( zoomController );

		freezeAvailable = freezeMax;

        playerFreezeSound = FMODUnity.RuntimeManager.CreateInstance("event:/player_freeze");
        cantFreezeSound = FMODUnity.RuntimeManager.CreateInstance("event:/cant_freeze");

        alreadyFrozen = false;
        alreadyCantFreeze = false;
    }

    void Update()
    {
        if (isFrozen)
        {
            freezeAvailable -= freezeDecline * Time.deltaTime;
        }
        else
        {
            freezeAvailable += freezeRegen * Time.deltaTime;
            alreadyFrozen = false;
        }


        freezeAvailable = Mathf.Clamp(freezeAvailable, 0, freezeMax);

        if (freezeAvailable != freezeMax)
            freezeBar.gameObject.SetActive(true);
        else
            freezeBar.gameObject.SetActive(false);

        freezeBar.value = freezeAvailable;

        if (freezeAvailable <= 0)
        {
            canFreeze = false;
            rb.velocity = oldVelocity;
        }
        else if (freezeAvailable >= freezeMax)
            canFreeze = true;

        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) && canFreeze)
            oldVelocity = rb.velocity;


        if ((Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Space)) && isFrozen)
        {
            canFreeze = false;
            rb.velocity = oldVelocity;
            
            //alreadyFrozen = false;
            //Debug.Log(alreadyFrozen);
        }

        if ( Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Space) ) {
            alreadyCantFreeze = false;
        }

        if ((Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space)) && canFreeze)
        {
            isFrozen = true;

        }
        else
        {
            isFrozen = false;
        }

        if ((Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space)) && canFreeze && !alreadyFrozen)
        {
            playerFreezeSound.start();
            alreadyFrozen = true;
            Debug.Log(alreadyFrozen);
        }

        if ((Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space)) && !canFreeze && !alreadyCantFreeze)
        {
            cantFreezeSound.start();
            alreadyCantFreeze = true;
        }
    }

	void FixedUpdate( )
	{
		if ( !isFrozen )
			zoomController.Zoom(rb.velocity.magnitude);

		if ( isFrozen )
			rb.velocity = Vector2.zero;

		if ( !isFrozen && freezeAccumulatedForce != Vector3.zero )
		{
			rb.AddForce( freezeAccumulatedForce );
			freezeAccumulatedForce = Vector3.zero;
		}
	}

	public void MadeShot( Quaternion angle )
	{
		if ( !isFrozen )
			rb.AddForce( angle * Vector2.left * shotForce + freezeAccumulatedForce );
		else
			freezeAccumulatedForce += angle * Vector2.left * shotForce;
	}
}
