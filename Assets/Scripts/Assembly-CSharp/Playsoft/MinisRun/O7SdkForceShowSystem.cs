using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class O7SdkForceShowSystem : ReactiveSystem<StateEntity>
	{
		public readonly UIContext uiContext;

		public readonly StateContext stateContext;

		public O7SdkForceShowSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkAgeGateShow.Added(), StateMatcher.O7SdkGenderGateShow.Added(), StateMatcher.O7SdkGdprShowAdProvidersSelection.Added(), StateMatcher.O7SdkGdprShowConsentTool.Added(), StateMatcher.O7SdkGdprDone.Added(), StateMatcher.O7SdkGdprShowDecider.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return !GameIntroHelper.IsIntroTime();
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				uiContext.uIEntity.tutorialLayer.layer.gameObject.SetActive(false);
				if (entity.isO7SdkAgeGateShow)
				{
					uiContext.OpenDialog<AgeGateScreen>();
				}
				if (entity.isO7SdkGenderGateShow)
				{
					uiContext.CloseDialog(ScreenId.AgeGateScreen);
					uiContext.OpenDialog<GenderGateScreen>();
				}
				if (entity.isO7SdkGdprShowConsentTool)
				{
					uiContext.CloseDialog(ScreenId.GenderGateScreen);
					uiContext.OpenDialog<GDPRSelectionScreen>();
				}
				if (entity.isO7SdkGdprShowAdProvidersSelection)
				{
					uiContext.CloseDialog(ScreenId.GDPRSelectionScreen);
					uiContext.OpenDialog<GDPRProvidersSelectionScreen>();
				}
				if (entity.isO7SdkGdprDone)
				{
					uiContext.CloseDialog(ScreenId.GDPRSelectionScreen);
					uiContext.CloseDialog(ScreenId.GDPRProvidersSelectionScreen);
					uiContext.uIEntity.tutorialLayer.layer.gameObject.SetActive(true);
					StateEntity stateEntity = stateContext.CreateEntity();
					stateEntity.isO7SdkForceCheckProcessed = true;
					stateEntity.isO7SdkGdprRequest = true;
				}
				if (entity.isO7SdkGdprShowDecider)
				{
					uiContext.CloseDialog(ScreenId.AgeGateScreen);
					uiContext.CloseDialog(ScreenId.GenderGateScreen);
					uiContext.uIEntity.tutorialLayer.layer.gameObject.SetActive(true);
					stateContext.sessionEntity.isO7SdkForceAgeGate = false;
				}
			}
		}
	}
}
