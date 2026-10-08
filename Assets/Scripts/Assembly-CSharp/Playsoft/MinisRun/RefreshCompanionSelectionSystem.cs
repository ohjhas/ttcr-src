using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshCompanionSelectionSystem : ReactiveSystem<StateEntity>
	{
		private readonly UIContext uiContext;

		public RefreshCompanionSelectionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.UpgradeCompanion.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasUpgradeCompanion;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				BaseDialog baseDialog = uiContext.FindCurrentScreen();
				if (baseDialog == null || (!(baseDialog is CompSelectScreen) && !(baseDialog is CompSelectInfoPopup)))
				{
					break;
				}
				if (baseDialog is CompSelectInfoPopup)
				{
					BaseDialog hud = uiContext.uIEntity.hUD.hud;
					UIEntity uIEntity = uiContext.CreateEntity();
					uIEntity.AddSetup(hud);
					uIEntity.AddUICompSingleRequest(entity.upgradeCompanion.CompanionId);
				}
				UIEntity uIEntity2 = uiContext.CreateEntity();
				uIEntity2.AddSetup(baseDialog);
				uIEntity2.AddUICompSingleRequest(entity.upgradeCompanion.CompanionId);
			}
		}
	}
}
