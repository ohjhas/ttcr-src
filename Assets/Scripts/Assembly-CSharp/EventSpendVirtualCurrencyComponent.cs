using Entitas;

[State]
public class EventSpendVirtualCurrencyComponent : IComponent
{
	public string currencyId;

	public string gameplayId;

	public int currencyChange;

	public int currencyBalance;
}
