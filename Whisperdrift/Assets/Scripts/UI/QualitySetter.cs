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

	static private int qualityIndex = -1;

	void Start()
	{
		text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		shadowText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

		if(qualityIndex <= -1) qualityIndex = QualitySettings.GetQualityLevel();
		QualitySettings.SetQualityLevel( qualityIndex );

		if(qualityIndex == 0)
		{
			text.text = shadowText.text = "Visual Quality: Low";
			postProcessLayer.enabled = false;
			postProcessVol.enabled = false;
			playerEffects.enabled = false;
		}
		else if(qualityIndex == 1)
		{
			text.text = shadowText.text = "Visual Quality: Medium";
			postProcessLayer.enabled = true;
			postProcessVol.enabled = true;
			playerEffects.enabled = true;
		}
		else if(qualityIndex == 2)
		{
			text.text = shadowText.text = "Visual Quality: High";
			postProcessLayer.enabled = true;
			postProcessVol.enabled = true;
			playerEffects.enabled = true;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
    {
		qualityIndex = (qualityIndex + 1 > 2) ? 0 : qualityIndex + 1;
		QualitySettings.SetQualityLevel( qualityIndex );

		if(qualityIndex == 0)
		{
			text.text = shadowText.text = "Visual Quality: Low";
			postProcessLayer.enabled = false;
			postProcessVol.enabled = false;
			playerEffects.enabled = false;
		}
		else if(qualityIndex == 1)
		{
			text.text = shadowText.text = "Visual Quality: Medium";
			postProcessLayer.enabled = true;
			postProcessVol.enabled = true;
			playerEffects.enabled = true;
		}
		else if(qualityIndex == 2)
		{
			text.text = shadowText.text = "Visual Quality: High";
			postProcessLayer.enabled = true;
			postProcessVol.enabled = true;
			playerEffects.enabled = true;
		}
    }
}
