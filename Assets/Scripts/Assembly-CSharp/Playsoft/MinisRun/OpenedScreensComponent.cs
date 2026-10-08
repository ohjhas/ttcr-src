using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class OpenedScreensComponent : IComponent
	{
		public List<BaseDialog> Dialogs;
	}
}
