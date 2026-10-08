using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class SkinChangeProp : AbstractProperty
	{
		public string WorldSuffix;

		public SkinChangeProp()
		{
		}

		public SkinChangeProp(SkinChangeProp skinChange)
		{
			IsEnabled = skinChange.IsEnabled;
			WorldSuffix = skinChange.WorldSuffix;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddChangeWorldSkin(WorldSuffix);
			}
		}
	}
}
