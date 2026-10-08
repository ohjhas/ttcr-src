namespace Playsoft.MinisRun
{
	public class LogTransactionsSystems : Feature
	{
		public LogTransactionsSystems(Contexts contexts)
			: base("Log Transactions Systems")
		{
			Add(new LogBoughtChestTransactionSystem(contexts));
			Add(new LogBoughtLevelTransactionSystem(contexts));
			Add(new LogReceivedPartsTransactionSystem(contexts));
			Add(new LogBoughtRestoreTransactionSystem(contexts));
			Add(new LogRewardVideoTransactionSystem(contexts));
			Add(new LogIAPPurchaseDoneSystem(contexts));
		}
	}
}
