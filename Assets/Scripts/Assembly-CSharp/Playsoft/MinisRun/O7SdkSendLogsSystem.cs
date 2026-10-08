using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkSendLogsSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		private O7SDKUtilityManager utilityManager;

		public O7SdkSendLogsSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		public void Initialize()
		{
			utilityManager = stateContext.o7SdkEntity.o7SdkUtilityManager.Manager;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkSendLogs);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isO7SdkCall;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				utilityManager.SendLogs(entity.o7SdkSendLogs.recipientEmail, entity.o7SdkSendLogs.subject, entity.o7SdkSendLogs.body, entity.o7SdkSendLogs.attachmentPaths, entity.o7SdkSendLogs.requestCode);
			}
		}
	}
}
