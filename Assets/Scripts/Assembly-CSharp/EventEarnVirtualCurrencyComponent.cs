using Entitas;

[State]
public class EventEarnVirtualCurrencyComponent : IComponent
{
	public string currencyId;

	public string gameplayId;

	public int currencyChange;

	public int currencyBalance;
}
