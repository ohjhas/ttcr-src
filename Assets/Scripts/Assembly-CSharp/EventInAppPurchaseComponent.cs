using Entitas;

[State]
public class EventInAppPurchaseComponent : IComponent
{
	public string productId;

	public string productName;

	public string currency;

	public int quantity;

	public int value;
}
