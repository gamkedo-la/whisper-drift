using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour
{
	[SerializeField] private GameObject parent = null;
	[SerializeField] private Renderer[] glowyParts = null;
	[SerializeField] private Material bigGlow = null;
	//[SerializeField] private GameObject hpBar = null;
	[SerializeField] private GameObject deathEffect = null;
	[SerializeField] private GameObject deathEffect2 = null;
	[SerializeField] private GameObject enemyPart = null;
	[SerializeField] private float touchDamage = 20f;
	[SerializeField] private GameObject[] eyes = null;
	[SerializeField] private float blinkTime = 0.05f;
	[SerializeField] private float blinkDelayMin = 3f;
	[SerializeField] private float blinkDelayMax = 5f;
	[SerializeField] private Color baseColor = Color.white;
	[SerializeField] private float glow = 6f;
	[SerializeField] private float glowMin = 1f;
	[SerializeField] private float pulseSpeed = 1f;

	private bool pulsing = false;
	private Material glowMat = null;

	void Start ()
	{
		Assert.IsNotNull( parent );
		Assert.IsNotNull( bigGlow );
		//Assert.IsNotNull( hpBar );
		Assert.IsNotNull( deathEffect );
		Assert.IsNotNull( deathEffect2 );
		Assert.IsNotNull( enemyPart );
		Assert.IsNotNull( eyes );
		Assert.AreNotEqual( eyes.Length, 0 );

		//hpBar.SetActive( false );

		Invoke( "StartBlink", Random.Range( blinkDelayMin, blinkDelayMax ) );
	}

	void Update( )
	{
		if ( pulsing )
		{
			float intensity = glowMin + Mathf.PingPong( Time.time * pulseSpeed, 1 ) * ( glow - glowMin );
			Color finalColor = baseColor * intensity;

			glowMat.SetColor( "_EmissionColor", finalColor );
			foreach ( var gl in glowyParts )
				gl.material = glowMat;
		}
	}

	public void GotHit( )
	{
		//if ( !hpBar.activeSelf )
		//hpBar.SetActive( true );
		foreach ( var gp in glowyParts )
		{
			gp.material = bigGlow;
		}

		glowMat = glowyParts[0].material;
		pulsing = true;
	}

	public void OnDeath()
	{
		Instantiate( deathEffect, transform.position, Quaternion.identity );
		Instantiate( deathEffect2, transform.position, Quaternion.identity );

		LevelManger.Instance.Faerie( );

		for ( int i = 0; i < Random.Range( 3, 5 ); i++ )
		{
			GameObject ep = Instantiate( enemyPart, transform.position + (Vector3)Random.insideUnitCircle * 0.2f, Quaternion.identity );
			ep.GetComponent<Rigidbody2D>( ).velocity = Quaternion.Euler( 0, 0, Random.Range( -60f, 60f ) ) * transform.up * Random.Range( 2f, 4f );
			ep.transform.localScale = new Vector3( Random.Range( 0.5f, 1f ), Random.Range( 0.5f, 1f ), 1.0f );
		}

		Destroy( parent );
	}

	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Player ) )
			return;

		collision.gameObject.GetComponent<HP>( ).ChangeHP( -touchDamage );
	}

	private void StartBlink( )
	{
		foreach ( var eye in eyes )
			eye.SetActive( false );

		Invoke( "EndBlink", blinkTime );
	}

	private void EndBlink( )
	{
		foreach ( var eye in eyes )
			eye.SetActive( true );

		Invoke( "StartBlink", Random.Range( blinkDelayMin, blinkDelayMax ) );
	}
}
