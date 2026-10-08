using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ClearPoolViewSystem : ReactiveSystem<InputEntity>
	{
		private readonly GameContext gameContext;

		public ClearPoolViewSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.hasDestroyOldSkin;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.DestroyOldSkin);
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				Dictionary<string, Stack<GameObject>> pools = gameContext.poolContainer.Pools;
				Dictionary<string, Stack<GameObject>>.Enumerator enumerator2 = pools.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.Key.Contains(entity.destroyOldSkin.oldSuffix))
					{
						ClearStack(enumerator2.Current.Value);
					}
				}
			}
		}

		private void ClearStack(Stack<GameObject> stack)
		{
			while (stack.Count > 0)
			{
				Object.Destroy(stack.Pop());
			}
		}
	}
}
