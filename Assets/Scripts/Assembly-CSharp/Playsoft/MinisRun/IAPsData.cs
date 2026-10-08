using System.Runtime.InteropServices;

namespace Playsoft.MinisRun
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct IAPsData
	{
		public int Id { get; set; }

		public string ProductId { get; set; }

		public string Name { get; set; }

		public int Amount { get; set; }

		public string Price { get; set; }

		public string Image { get; set; }
	}
}
