using UnityEngine;
using UnityEngine.Assertions;

public class Crystal : MonoBehaviour
{
	[SerializeField] private GameObject endEffect = null;
	[SerializeField] private Light mainLight = null;
	[SerializeField] private Renderer[] glows = null;
	[SerializeField] private Color baseColor = Color.white;
	[SerializeField] private float glow = 1.0f;
	[SerializeField] private float lightInt = 0.2f;
	[SerializeField] private int hp = 3;
	[SerializeField] private GameObject[] parts = null;

	private Material glowMat = null;
	private int currentHp;

	void Start( )
	{
		Assert.IsNotNull( endEffect );
		Assert.IsNotNull( mainLight );
		Assert.IsNotNull( glows );
		Assert.IsNotNull( parts );

		glowMat = glows[0].material;
		currentHp = hp;
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		if ( !collision.gameObject.CompareTag( Tags.Projectile ) && !collision.gameObject.CompareTag( Tags.Player ) )
			return;

		currentHp--;
		if ( currentHp <= 0 )
			Destroy( );

		float multi = 1 + hp - currentHp;
		float brightness = lightInt * multi;
		float emission = glow * multi;
		Color finalColor = baseColor * emission;

		mainLight.intensity = brightness;

		glowMat.SetColor( "_EmissionColor", finalColor );
		foreach ( var gl in glows )
			gl.material = glowMat;
	}

	private void Destroy( )
	{
		Instantiate( endEffect, transform.position, Quaternion.identity );

		for ( int i = 0; i < Random.Range( 4, 6 ); i++ )
		{
			GameObject ep = Instantiate( parts[Random.Range(0, parts.Length)], transform.position + (Vector3)Random.insideUnitCircle * 0.2f, Quaternion.identity );
			ep.GetComponent<Rigidbody2D>( ).velocity = Quaternion.Euler( 0, 0, Random.Range( -60f, 60f ) ) * transform.up * Random.Range( 3f, 5f );
			ep.transform.localScale = new Vector3( Random.Range( 0.3f, 0.6f ), Random.Range( 0.3f, 0.6f ), 1.0f );
		}

		Destroy( gameObject );
	}
}
