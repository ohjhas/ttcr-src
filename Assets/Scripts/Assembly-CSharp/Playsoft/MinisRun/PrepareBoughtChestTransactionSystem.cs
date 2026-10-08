using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PrepareBoughtChestTransactionSystem : ReactiveSystem<StateEntity>
	{
		private StateContext stateContext;

		public PrepareBoughtChestTransactionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionValidated.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isTransaction && entity.hasBoughtChest && !entity.hasBoughtChestPrepared;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				List<GeneratedReward> newPreparedChestRewards = GenerateReward(entity.boughtChest.Chest);
				entity.AddBoughtChestPrepared(newPreparedChestRewards);
			}
		}

		private List<GeneratedReward> GenerateReward(ChestData chest)
		{
			int partsAmount = UnityEngine.Random.Range(chest.MinPartsAmount, chest.MaxPartsAmount);
			List<ChestRewardData> partsRewards;
			List<ChestRewardData> otherRewards;
			SegregateRewardData(chest, out partsRewards, out otherRewards);
			List<ChestRewardData> rewardsData = GenerateRewardsList(partsRewards, otherRewards, partsAmount);
			return ConvertDataToReward(rewardsData);
		}

		private List<ChestRewardData> GenerateRewardsList(List<ChestRewardData> partsRewards, List<ChestRewardData> otherRewards, int partsAmount)
		{
			List<ChestRewardData> list = new List<ChestRewardData>();
			AddGuarantedPartsRewards(partsRewards, list);
			if (partsRewards.Count > 0)
			{
				for (int i = 0; i < partsAmount; i++)
				{
					float randomValue = UnityEngine.Random.Range(0f, 100f);
					ChestRewardData item = SelectPartFromList(partsRewards, randomValue);
					list.Add(item);
				}
			}
			for (int j = 0; j < otherRewards.Count; j++)
			{
				float num = UnityEngine.Random.Range(0f, 100f);
				if (num <= otherRewards[j].Probability)
				{
					list.Add(otherRewards[j]);
				}
			}
			return list;
		}

		private ChestRewardData SelectPartFromList(List<ChestRewardData> partsRewards, float randomValue)
		{
			float num = 0f;
			for (int i = 0; i < partsRewards.Count; i++)
			{
				num += partsRewards[i].Probability;
				if (randomValue <= num)
				{
					return partsRewards[i];
				}
			}
			return partsRewards[partsRewards.Count - 1];
		}

		private void AddGuarantedPartsRewards(List<ChestRewardData> partsRewards, List<ChestRewardData> generatedRewards)
		{
			if (partsRewards.Count <= 0)
			{
				return;
			}
			for (int num = partsRewards.Count - 1; num >= 0; num--)
			{
				if (partsRewards[num].Probability >= 100f)
				{
					generatedRewards.Add(partsRewards[num]);
					partsRewards.RemoveAt(num);
				}
			}
		}

		private void SegregateRewardData(ChestData chest, out List<ChestRewardData> partsRewards, out List<ChestRewardData> otherRewards)
		{
			partsRewards = new List<ChestRewardData>();
			otherRewards = new List<ChestRewardData>();
			for (int i = 0; i < chest.RewardsData.Count; i++)
			{
				if (chest.RewardsData[i].Group == GroupType.Parts)
				{
					partsRewards.Add(chest.RewardsData[i]);
				}
				else
				{
					otherRewards.Add(chest.RewardsData[i]);
				}
			}
			partsRewards.Sort((ChestRewardData ch1, ChestRewardData ch2) => ch1.Probability.CompareTo(ch2.Probability));
		}

		private List<GeneratedReward> ConvertDataToReward(List<ChestRewardData> rewardsData)
		{
			List<GeneratedReward> list = new List<GeneratedReward>();
			for (int i = 0; i < rewardsData.Count; i++)
			{
				int amount = Mathf.RoundToInt(UnityEngine.Random.Range((float)rewardsData[i].AmountMin, (float)rewardsData[i].AmountMax));
				if (rewardsData[i].Group == GroupType.Parts)
				{
					string text = FindPartName(rewardsData[i].Type, rewardsData[i].Rarity, rewardsData[i].Index);
					if (!string.IsNullOrEmpty(text))
					{
						GeneratedReward newReward = new GeneratedReward(rewardsData[i].Type, rewardsData[i].Rarity, rewardsData[i].Index, amount, text);
						AddRewardToList(list, newReward);
					}
				}
				else
				{
					GeneratedReward newReward = new GeneratedReward(rewardsData[i].Type, rewardsData[i].Rarity, rewardsData[i].Index, amount, string.Empty);
					AddRewardToList(list, newReward);
				}
			}
			return list;
		}

		private void AddRewardToList(List<GeneratedReward> finalRewards, GeneratedReward newReward)
		{
			GeneratedReward generatedReward = finalRewards.Find((GeneratedReward reward) => !reward.Type.Equals(ItemType.Currency) && reward.Type.Equals(newReward.Type) && reward.OptionalName.Equals(newReward.OptionalName));
			if (generatedReward != null)
			{
				generatedReward.Amount += newReward.Amount;
			}
			else
			{
				finalRewards.Add(newReward);
			}
		}

		private string FindPartName(ItemType itemType, Rarity rarity, int index)
		{
			switch (itemType)
			{
			case ItemType.Companion:
			{
				List<string> listOfCharNames = GetListOfCompanionNames(rarity);
				return listOfCharNames[UnityEngine.Random.Range(0, listOfCharNames.Count)];
			}
			case ItemType.Character:
			{
				List<string> listOfCharNames = GetListOfCharNames(rarity, index);
				if (listOfCharNames.Count == 0)
				{
					return string.Empty;
				}
				return listOfCharNames[UnityEngine.Random.Range(0, listOfCharNames.Count)];
			}
			default:
				return string.Empty;
			}
		}

		private List<string> GetListOfCharNames(Rarity rarity, int index)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("PlayerLevelNumber").SingleEntity();
			List<string> list = new List<string>();
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			while (list.Count == 0 && !rarity.Equals(Rarity.None))
			{
				Dictionary<int, CharactersStaticBalancingData>.Enumerator enumerator = balancingDataEntity.characterStaticBalancingData.Map.GetEnumerator();
				while (enumerator.MoveNext())
				{
					bool flag = index < 0 && enumerator.Current.Value.RARITY == rarity && stateEntity.integer.Amount >= enumerator.Current.Value.Minlevel;
					bool flag2 = index >= 0 && enumerator.Current.Value.ID == index;
					if (flag || flag2)
					{
						string name = "CharUpgradeData" + enumerator.Current.Value.ID;
						int amount = stateContext.GetEntitiesWithName(name).SingleEntity().integer.Amount;
						int count = DataProviderHelper.GetCharData(enumerator.Current.Value.ID).Upgrades.Count;
						if (amount < count)
						{
							list.Add(enumerator.Current.Value.Name);
						}
					}
				}
				if (list.Count == 0)
				{
					Debug.LogWarning("[Chest] No character available from specific rarity");
					rarity = GetMoreCommonRarity(rarity);
				}
				enumerator.Dispose();
			}
			return list;
		}

		private List<string> GetListOfCompanionNames(Rarity rarity)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("PlayerLevelNumber").SingleEntity();
			List<string> list = new List<string>();
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			while (list.Count == 0 && !rarity.Equals(Rarity.None))
			{
				Dictionary<int, CompanionsStaticBalancingData>.Enumerator enumerator = balancingDataEntity.companionsStaticBalancingData.Map.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Value.RARITY == rarity && stateEntity.integer.Amount >= enumerator.Current.Value.Minlevel)
					{
						list.Add(enumerator.Current.Value.Name);
					}
				}
				if (list.Count == 0)
				{
					Debug.LogWarning("[Chest] No companion available from specific rarity");
					rarity = GetMoreCommonRarity(rarity);
				}
				enumerator.Dispose();
			}
			return list;
		}

		private Rarity GetMoreCommonRarity(Rarity rarity)
		{
			return (Rarity)Mathf.Clamp((int)(rarity - 1), 0, Enum.GetNames(typeof(Rarity)).Length - 1);
		}
	}
}
