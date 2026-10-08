namespace Playsoft.MinisRun
{
	public class TransactionSystems : Feature
	{
		public TransactionSystems(Contexts contexts)
			: base("Transaction Systems")
		{
			Add(new BeginValidationTransactionSystem(contexts));
			Add(new ReplaceChestForTutorialTransactionSystem(contexts));
			Add(new PrepareBoughtChestTransactionSystem(contexts));
			Add(new GrantBoughtChestTransactionSystem(contexts));
			Add(new GrantBoughtCurrencyTransactionSystem(contexts));
			Add(new GrantBoughtLevelTransactionSystem(contexts));
			Add(new GrantBoughtPlayerRestoreSystem(contexts));
			Add(new GrantBoughtRefillTransactionSystem(contexts));
			Add(new GrantTransactionFinalizeSystem(contexts));
			Add(new ConsumeCurrencyPriceTransactionSystem(contexts));
			Add(new ConsumePartsPriceTransactionSystem(contexts));
			Add(new LogTransactionsSystems(contexts));
			Add(new ConsumeVideoTransactionSystem(contexts));
			Add(new TransactionFinalizeSystem(contexts));
		}
	}
}
