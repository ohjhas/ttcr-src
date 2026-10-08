using System.Collections.Generic;
using Entitas;
using I2.Loc;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataChapterSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string chaptersBalancingPath = "BalancingData/ChaptersBalancing";

		public BalancingDataChapterSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasChapterBalancingData)
			{
				AddChapterData(balancingDataEntity);
			}
		}

		private void AddChapterData(StateEntity balancingEntity)
		{
			balancingEntity.AddChapterBalancingData(new Dictionary<int, ChaptersBalancingData>());
			ChaptersBalancing chaptersBalancing = Resources.Load("BalancingData/ChaptersBalancing") as ChaptersBalancing;
			ChaptersBalancingData[] dataArray = chaptersBalancing.DataArray;
			foreach (ChaptersBalancingData chaptersBalancingData in dataArray)
			{
				chaptersBalancingData.Chaptername = ScriptLocalization.Get(chaptersBalancingData.Chaptername);
				balancingEntity.chapterBalancingData.Map.Add(chaptersBalancingData.Chapterid, chaptersBalancingData);
			}
		}
	}
}
