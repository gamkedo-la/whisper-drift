using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class QualitySetter : MonoBehaviour, IPointerClickHandler
{
	public PostProcessLayer postProcessLayer;
	public PostProcessVolume postProcessVol;
	
	public PlayerEffects playerEffects;
	
	private TextMeshProUGUI text;
	private TextMeshProUGUI shadowText;
	
	void Start()
	{
		text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		shadowText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
	}
	
	public void OnPointerClick(PointerEventData eventData)
    {
		int qualityIndex = QualitySettings.GetQualityLevel();
		QualitySettings.SetQualityLevel( (qualityIndex + 1 > 2) ? 0 : qualityIndex + 1 );
		
		if(qualityIndex == 0)
		{
			text.text = shadowText.text = "Quality: Low";
			postProcessLayer.enabled = false;
			postProcessVol.enabled = false;
			playerEffects.enabled = false;
		}
		else if(qualityIndex == 1)
		{
			text.text = shadowText.text = "Quality: Medium";
			postProcessLayer.enabled = true;
			postProcessVol.enabled = true;
			playerEffects.enabled = true;
		}
		else if(qualityIndex == 2)
		{
			text.text = shadowText.text = "Quality: High";
			postProcessLayer.enabled = true;
			postProcessVol.enabled = true;
			playerEffects.enabled = true;
		}
    }
}
