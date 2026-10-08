using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIChapterListResponseComponent : IComponent
	{
		public List<WSChapterData> ChapterDataList;
	}
}
