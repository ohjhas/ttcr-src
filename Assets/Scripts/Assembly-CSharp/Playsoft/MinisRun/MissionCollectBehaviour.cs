using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class MissionCollectBehaviour : MonoBehaviour, IMissionUpdateListener
	{
		[SerializeField]
		private GameObject Container;

		[SerializeField]
		private Image TokenImage;

		[SerializeField]
		private TextMeshProUGUI CounterLabel;

		private bool isInitialized;

		private void Awake()
		{
			Container.SetActive(false);
		}

		private void Update()
		{
			if (!isInitialized && Contexts.sharedInstance.game.hasWorld)
			{
				Init();
			}
		}

		public void OnMissionUpdate(int value, int firstThreshold, int secondThreshold, int thirdThreshold)
		{
			CounterLabel.text = CreateCounterText(value, thirdThreshold);
		}

		private void Init()
		{
			isInitialized = true;
			GameContext game = Contexts.sharedInstance.game;
			GameEntity worldEntity = game.worldEntity;
			if (worldEntity.hasMissionType && worldEntity.missionType.Type != MissionType.Have_Stamina)
			{
				Container.SetActive(true);
				game.CreateEntity().AddMissionUpdateListener(this);
				OnMissionUpdate(worldEntity.missionCounter.Value, worldEntity.missionThresholds.FirstStar, worldEntity.missionThresholds.SecondStar, worldEntity.missionThresholds.ThirdStar);
				SelectImage(worldEntity);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}

		private void SelectImage(GameEntity world)
		{
			if (world.hasMissionSprite)
			{
				TokenImage.gameObject.SetActive(true);
				TokenImage.sprite = world.missionSprite.Sprite;
				TokenImage.transform.localScale = Vector3.one * world.missionSprite.Size;
			}
			else
			{
				TokenImage.gameObject.SetActive(false);
			}
		}

		private string CreateCounterText(int collectedTokens, int maxNumberOfTokens)
		{
			return Mathf.Min(collectedTokens, maxNumberOfTokens) + "/" + maxNumberOfTokens;
		}
	}
}
