public interface IBalancingSheet<T> : IBalancingSheet
{
	string SheetName { get; set; }

	string WorksheetName { get; set; }

	T[] DataArray { get; set; }
}
public interface IBalancingSheet
{
}
