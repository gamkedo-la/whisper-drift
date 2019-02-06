using UnityEngine;
using UnityEngine.Assertions;

public class Pulsing : MonoBehaviour
{
	[SerializeField] private Renderer objRenderer = null;
	[SerializeField] private Color color = Color.white;
	[SerializeField] private float minIntensily = 1f;
	[SerializeField] private float maxIntensily = 3f;
	[SerializeField] private float speed = 0.25f;

	private Material material;

	void Start ()
	{
		Assert.IsNotNull( objRenderer );
		material = objRenderer.material;
	}

	void Update ()
	{
		float intensity = minIntensily + Mathf.PingPong( Time.time * speed, 1 ) * ( maxIntensily - minIntensily );
		material.color = color * intensity;
	}
}
