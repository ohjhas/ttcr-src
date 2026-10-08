using System;
using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIActionFinishedCallbackComponent : IComponent
	{
		public Action Callback;
	}
}
