using Playsoft.MinisRun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgressionBar : BaseUI
{
	[SerializeField]
	private TextMeshProUGUI levelNumberText;

	[SerializeField]
	private Image fillBarImage;

	public override void SetData(UIEntity parameters)
	{
		base.SetData(parameters);
		if (parameters.hasUIPlayerProgressionStateResponse)
		{
			UIPlayerProgressionStateResponseComponent uIPlayerProgressionStateResponse = parameters.uIPlayerProgressionStateResponse;
			PlayerProgressionChanged(uIPlayerProgressionStateResponse.PlayerLevel, uIPlayerProgressionStateResponse.PlayerExperience, uIPlayerProgressionStateResponse.CurrentRequiredExp, uIPlayerProgressionStateResponse.NextRequiredExp);
		}
	}

	public void PlayerProgressionChanged(int currentLevel, int currenctExperience, int currentLevelRequiredExp, int nextLevelRequiredExp)
	{
		if (!(null == levelNumberText))
		{
			levelNumberText.text = currentLevel.ToString();
			fillBarImage.fillAmount = Mathf.InverseLerp(currentLevelRequiredExp, nextLevelRequiredExp, currenctExperience);
		}
	}
}
