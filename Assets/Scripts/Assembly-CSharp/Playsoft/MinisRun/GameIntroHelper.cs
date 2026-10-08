using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public static class GameIntroHelper
	{
		public static bool IsIntroTime()
		{
			HashSet<StateEntity> entitiesWithName = Contexts.sharedInstance.state.GetEntitiesWithName("UnlockedLevel");
			if (entitiesWithName.Count > 0)
			{
				StateEntity stateEntity = entitiesWithName.SingleEntity();
				return stateEntity.integer.Amount <= 0;
			}
			return false;
		}
	}
}
