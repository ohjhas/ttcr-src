using System.Collections.Generic;
using Entitas;
using O7SDK;

public class O7SdkCountryCodeSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
{
	private readonly StateContext stateContext;

	private O7SDKGateManager gateManager;

	private O7SDKDataManager dataManager;

	public O7SdkCountryCodeSystem(Contexts contexts)
		: base((IContext<StateEntity>)contexts.state)
	{
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		gateManager = stateContext.o7SdkEntity.o7SdkAgeGateManager.Manager;
		dataManager = stateContext.o7SdkEntity.o7SdkDataManager.Manager;
		if (!stateContext.o7SdkEntity.hasO7SdkCountryCodes)
		{
			string validCountryCodes = dataManager.GetValidCountryCodes();
			List<string> newCodes = (string.IsNullOrEmpty(validCountryCodes) ? new List<string>() : O7SDKTypesMarshaling.ToList<string>(validCountryCodes));
			stateContext.o7SdkEntity.AddO7SdkCountryCodes(newCodes);
		}
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.O7SdkSelectCountryCode.Added(), StateMatcher.O7SdkGridDownloaded.Added());
	}

	protected override bool Filter(StateEntity entity)
	{
		return true;
	}

	protected override void Execute(List<StateEntity> entities)
	{
		foreach (StateEntity entity in entities)
		{
			if (entity.isO7SdkGridDownloaded)
			{
				FetchCountryCode();
			}
			if (entity.hasO7SdkSelectCountryCode)
			{
				gateManager.SetCountryCodeOverride(entity.o7SdkSelectCountryCode.countryCode);
			}
		}
	}

	private void SetCountryCode(string code = null)
	{
		StateEntity stateEntity = stateContext.CreateEntity();
		stateEntity.isSavedData = true;
		stateEntity.AddName("countryCodeSendKey");
		stateEntity.AddStringData(code);
	}

	private void FetchCountryCode()
	{
		if (!gateManager.MustShowCountryCodeOverrideEnabled())
		{
			SetCountryCode("NotShow");
			return;
		}
		string currentCountryCode = gateManager.GetCurrentCountryCode();
		SetCountryCode(currentCountryCode);
	}
}
