using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ChapterListDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public ChapterListDataProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUIChapterListRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			List<WSChapterData> newChapterDataList = CreateChapterDataList();
			foreach (UIEntity entity in entities)
			{
				entity.AddUIChapterListResponse(newChapterDataList);
			}
		}

		private List<WSChapterData> CreateChapterDataList()
		{
			List<WSChapterData> list = new List<WSChapterData>();
			ChapterSettingsSO[] chapterSettings = SettingsService.SettingsSO.ChapterSettings;
			for (int i = 0; i < chapterSettings.Length; i++)
			{
				if (stateContext.balancingDataEntity.chapterBalancingData.Map.ContainsKey(i))
				{
					ChaptersBalancingData chaptersBalancingData = stateContext.balancingDataEntity.chapterBalancingData.Map[i];
					WSChapterData item = new WSChapterData(chaptersBalancingData.Chapterid, chaptersBalancingData.Chaptername, chaptersBalancingData.Requiredstars);
					list.Add(item);
				}
			}
			return list;
		}
	}
}
