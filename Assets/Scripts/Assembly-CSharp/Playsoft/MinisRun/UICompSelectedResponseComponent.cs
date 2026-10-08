using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UICompSelectedResponseComponent : IComponent
	{
		public int SelectedCompId;

		public bool IsSelected;
	}
}
