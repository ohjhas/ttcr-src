using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshCharacterSelectionSystem : ReactiveSystem<StateEntity>
	{
		private readonly UIContext uiContext;

		public RefreshCharacterSelectionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.UpgradeCharacter.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasUpgradeCharacter;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				BaseDialog baseDialog = uiContext.FindCurrentScreen();
				if (baseDialog == null || (!(baseDialog is CharSelectScreen) && !(baseDialog is CharSelectInfoPopup)))
				{
					break;
				}
				if (baseDialog is CharSelectInfoPopup)
				{
					BaseDialog hud = uiContext.uIEntity.hUD.hud;
					UIEntity uIEntity = uiContext.CreateEntity();
					uIEntity.AddSetup(hud);
					uIEntity.AddUICharSingleRequest(entity.upgradeCharacter.CharacterId);
				}
				UIEntity uIEntity2 = uiContext.CreateEntity();
				uIEntity2.AddSetup(baseDialog);
				uIEntity2.AddUICharSingleRequest(entity.upgradeCharacter.CharacterId);
			}
		}
	}
}
