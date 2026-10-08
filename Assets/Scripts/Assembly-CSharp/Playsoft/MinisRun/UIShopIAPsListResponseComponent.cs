using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIShopIAPsListResponseComponent : IComponent
	{
		public List<IAPsData> IAPsDataList;
	}
}
