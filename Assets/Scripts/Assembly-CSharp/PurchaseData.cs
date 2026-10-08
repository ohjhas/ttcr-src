using System;

[Serializable]
public class PurchaseData
{
	public string orderId;

	public PurchaseState purchaseState;

	public string itemId;

	public int quantity;

	public long purchaseTime;

	public string developerPayload;

	public bool justRestore;

	public string purchaseToken;
}
