using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class TutorialPossibleGroupsComponent : IComponent
{
	public Dictionary<string, List<TutorialBaseStep>> StepsGroups;
}
