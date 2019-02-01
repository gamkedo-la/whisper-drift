using UnityEngine;
using UnityEngine.Assertions;

public class PlayerEffects : MonoBehaviour
{
	[SerializeField] private Light mainLight = null;
	[SerializeField] private float brightnessMin = 0.2f;
	[SerializeField] private float brightnessMax = 0.7f;

	[SerializeField] private Renderer mainRenderer = null;
	[SerializeField] private Renderer particlesConstant = null;
	[SerializeField] private Renderer particlesFollow = null;
	[SerializeField] private Color baseColor = Color.white;
	[SerializeField] private float glowMin = 1.0f;
	[SerializeField] private float glowMax = 1.8f;

	private Material glowMat = null;

	void Start( )
	{
		Assert.IsNotNull( mainLight );
		Assert.IsNotNull( mainRenderer );
		Assert.IsNotNull( particlesConstant );
		Assert.IsNotNull( particlesFollow );

		glowMat = mainRenderer.material;
		Assert.IsNotNull( glowMat );

		LevelProgress( 0 );
	}

	public void LevelProgress( float progress )
	{
		float brightnessToAdd = ( brightnessMax - brightnessMin ) * progress;
		mainLight.intensity = brightnessMin + brightnessToAdd;

		float emission = glowMin + ( ( glowMax - glowMin ) * progress );
		Color finalColor = baseColor * emission;
		glowMat.SetColor( "_EmissionColor", finalColor );

		particlesConstant.material = glowMat;
		particlesFollow.material = glowMat;
	}
}
