using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
	public float CurrentHP { get; private set; }
	public float MaxHP { get { return maxHP; } }

	[Header("External objects")]
	[SerializeField, Tooltip("An optional Slider that acts as a HP Bar.")] private Slider hpBar = null;

	[Header("Tweakable")]
	[SerializeField] private float maxHP = 10;
	[SerializeField] private bool destroyOnNoHP = false;

	[Header("Events")]
	[SerializeField] private UnityEvent onHealthChange = null;
	[SerializeField] private UnityEvent onDeath = null;

    private FMOD.Studio.EventInstance playerHitSound;
    private FMOD.Studio.EventInstance playerDeathSound;

    void Start( )
	{
		CurrentHP = maxHP;

		if ( hpBar )
		{
			hpBar.maxValue = maxHP;
			hpBar.value = maxHP;
		}

        playerHitSound = FMODUnity.RuntimeManager.CreateInstance("event:/player_hit");
        playerDeathSound = FMODUnity.RuntimeManager.CreateInstance("event:/player_death");
    }

	/// <summary>
	/// Changes current HP. Respects HP restrictions and fires events if necessary.
	/// </summary>
	/// <param name="change">Amount of HP change. Negative values for damage, positive for healing.</param>
	public void ChangeHP( float change )
	{
		CurrentHP += change;
        if (CurrentHP > 0 && CurrentHP != 100 && gameObject.name == "Player")
        {
            playerHitSound.start();
        } else if (CurrentHP <= 0 && gameObject.name == "Player")
        {
            playerDeathSound.start();
        }
		CurrentHP = CurrentHP > maxHP ? maxHP : CurrentHP;
		CurrentHP = CurrentHP < 0 ? 0 : CurrentHP;

		onHealthChange.Invoke( );

		if ( hpBar )
			hpBar.value = CurrentHP;

		if ( CurrentHP <= 0 )
			DestroyMe( );
	}

	private void DestroyMe( )
	{
		onDeath.Invoke( );

		if ( destroyOnNoHP )
			Destroy( gameObject );
	}
}
