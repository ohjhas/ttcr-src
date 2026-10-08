using System;
using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

namespace O7SDK
{
	public class O7SdkPurchaseValidationSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uIContext;

		public O7SdkPurchaseValidationSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
			uIContext = context.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkIapBuySuccess.Added(), StateMatcher.O7SdkIapBuyFail.Added(), StateMatcher.O7SdkIapBuyError.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasO7SdkIapBuyError || entity.hasO7SdkIapBuyFail || entity.hasO7SdkIapBuySuccess;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				HideSpinner();
				if (entity.hasO7SdkIapBuyError)
				{
					ShowExplanation(ExplanationType.IapCancel);
				}
				if (entity.hasO7SdkIapBuyFail)
				{
					ShowExplanation(ExplanationType.IapFail);
				}
				if (entity.hasO7SdkIapBuySuccess)
				{
					SavePaidUserInfo();
					UpdatePurchaseSuccessOnUi(entity.o7SdkIapBuySuccess.productId, entity.o7SdkIapBuySuccess.transform);
				}
				entity.isDestroyed = true;
			}
		}

		private void UpdatePurchaseSuccessOnUi(string id, RectTransform rectTransform)
		{
			List<IapPacks> iapPacks = stateContext.o7SdkEntity.o7SdkIapPacks.iapPacks;
			if (iapPacks == null)
			{
				return;
			}
			IapPacks iapPacks2 = iapPacks.Find((IapPacks x) => x.id == id);
			if (iapPacks2 == null)
			{
				return;
			}
			StateEntity stateEntity = stateContext.CreateEntity();
			foreach (Item item in iapPacks2.items)
			{
				if (item.name.Equals(ProjectConstants.HARD_CURRENCY_NAME))
				{
					stateEntity.ReplaceBoughtHardCurrency(Convert.ToInt32(item.amount));
				}
			}
			stateEntity.isTransactionBegin = true;
			stateEntity.isTransaction = true;
			stateEntity.AddTransactionName(id, id);
			stateEntity.AddTransactionOrigin(rectTransform);
		}

		private void SavePaidUserInfo()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("PaidUserSend");
			stateEntity.AddBoolean(true);
		}

		private void HideSpinner()
		{
			Debug.Log("Closing Spinner Request");
			UIEntity uIEntity = uIContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.isLoadingSpinnerHide = true;
		}

		private void ShowExplanation(ExplanationType explanationType)
		{
			UIEntity uIEntity = uIContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddUIDisplayExplanation(explanationType);
		}
	}
}
