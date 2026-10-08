using UnityEngine;

public class MissionStaminaHUD : BaseUI, IMissionUpdateListener
{
	[SerializeField]
	private GameObject container;

	[SerializeField]
	private GameObject star1;

	[SerializeField]
	private GameObject star2;

	[SerializeField]
	private GameObject star3;

	[SerializeField]
	private GameObject stamina0;

	[SerializeField]
	private GameObject stamina100;

	private const int maxStaminaPercent = 100;

	private bool isInitialized;

	private void Update()
	{
		if (!isInitialized && Contexts.sharedInstance.game.hasWorld)
		{
			Init();
		}
	}

	private void Init()
	{
		isInitialized = true;
		GameContext game = Contexts.sharedInstance.game;
		GameEntity worldEntity = game.worldEntity;
		if (worldEntity.hasMissionType && worldEntity.missionType.Type == MissionType.Have_Stamina)
		{
			container.SetActive(true);
			game.CreateEntity().AddMissionUpdateListener(this);
			PositionStars(worldEntity.missionThresholds.FirstStar, worldEntity.missionThresholds.SecondStar, worldEntity.missionThresholds.ThirdStar);
			OnMissionUpdate(100, worldEntity.missionThresholds.FirstStar, worldEntity.missionThresholds.SecondStar, worldEntity.missionThresholds.ThirdStar);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	private void PositionStars(int firstStar, int secondStar, int thirdStar)
	{
		float x = stamina0.GetComponent<RectTransform>().localPosition.x;
		float x2 = stamina100.GetComponent<RectTransform>().localPosition.x;
		Vector3 localPosition = star1.transform.parent.GetComponent<RectTransform>().localPosition;
		Vector3 localPosition2 = star2.transform.parent.GetComponent<RectTransform>().localPosition;
		Vector3 localPosition3 = star3.transform.parent.GetComponent<RectTransform>().localPosition;
		localPosition.x = x;
		localPosition2.x = (x2 - x) * (float)secondStar / 100f + x;
		localPosition3.x = (x2 - x) * (float)thirdStar / 100f + x;
		star1.transform.parent.GetComponent<RectTransform>().localPosition = localPosition;
		star2.transform.parent.GetComponent<RectTransform>().localPosition = localPosition2;
		star3.transform.parent.GetComponent<RectTransform>().localPosition = localPosition3;
	}

	public void OnMissionUpdate(int value, int firstThreshold, int secondThreshold, int thirdThreshold)
	{
		star1.SetActive(value > 0);
		star2.SetActive(value >= secondThreshold);
		star3.SetActive(value >= thirdThreshold);
	}
}
