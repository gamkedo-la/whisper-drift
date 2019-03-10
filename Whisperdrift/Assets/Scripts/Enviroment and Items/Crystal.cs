using UnityEngine;
using UnityEngine.Assertions;

public class Crystal : MonoBehaviour
{
	[SerializeField] private GameObject endEffect = null;
	[SerializeField] private Renderer[] glows = null;
	[SerializeField] private Color baseColor = Color.white;
	[SerializeField] private float glow = 6f;
	[SerializeField] private float glowMin = 1f;
	[SerializeField] private float pulseSpeed = 1f;
	[SerializeField] private int hp = 3;
	[SerializeField] private GameObject[] parts = null;

	private Material glowMat = null;
	private bool pulsing = false;
	private int currentHp;

	void Start( )
	{
		Assert.IsNotNull( endEffect );
		Assert.IsNotNull( glows );
		Assert.IsNotNull( parts );

		glowMat = glows[0].material;
		currentHp = hp;

	}

	void Update( )
	{
		if ( pulsing )
		{
			float intensity = glowMin + Mathf.PingPong( Time.time * pulseSpeed, 1 ) * ( glow - glowMin );
			Color finalColor = baseColor * intensity;

			glowMat.SetColor( "_EmissionColor", finalColor );
			foreach ( var gl in glows )
				gl.material = glowMat;
		}
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Projectile ) && !collision.gameObject.CompareTag( Tags.Player ) )
			return;

		currentHp--;
		if ( currentHp <= 0 )
			Destroy( );

		pulsing = true;

		float multi = 1 + hp - currentHp;
		float emission = glow * multi;
		Color finalColor = baseColor * emission;

		glowMat.SetColor( "_EmissionColor", finalColor );
		foreach ( var gl in glows )
			gl.material = glowMat;
	}

	private void Destroy( )
	{
		Instantiate( endEffect, transform.position, Quaternion.identity );

		LevelManger.Instance.Faerie( );

		for ( int i = 0; i < Random.Range( 4, 6 ); i++ )
		{
			GameObject ep = Instantiate( parts[Random.Range(0, parts.Length)], transform.position + (Vector3)Random.insideUnitCircle * 0.2f, Quaternion.identity );
			ep.GetComponent<Rigidbody2D>( ).velocity = Quaternion.Euler( 0, 0, Random.Range( -60f, 60f ) ) * transform.up * Random.Range( 3f, 5f );
			ep.transform.localScale = new Vector3( Random.Range( 0.3f, 0.6f ), Random.Range( 0.3f, 0.6f ), 1.0f );
			ep.transform.localRotation = Quaternion.Euler( 0, 0, Random.Range( 0, 360f ) );
		}

		Destroy( gameObject );
	}
}
