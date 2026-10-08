using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIPlayerProgressionStateResponseComponent : IComponent
	{
		public int PlayerLevel;

		public int PlayerExperience;

		public int CurrentRequiredExp;

		public int NextRequiredExp;
	}
}
