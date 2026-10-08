using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class AsyncLoadSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		private IGroup<GameEntity> gameAsyncRequestsGroup;

		private IGroup<UIEntity> uiAsyncRequestsGroup;

		public AsyncLoadSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			stateContext = contexts.state;
			uiContext = contexts.uI;
			gameAsyncRequestsGroup = gameContext.GetGroup(GameMatcher.AsyncRequest);
			uiAsyncRequestsGroup = uiContext.GetGroup(UIMatcher.AsyncRequest);
		}

		public void Execute()
		{
			float num = 0f;
			float num2 = uiAsyncRequestsGroup.count + gameAsyncRequestsGroup.count;
			UIEntity[] entities = uiAsyncRequestsGroup.GetEntities();
			foreach (UIEntity request in entities)
			{
				num += CheckUiRequest(request);
			}
			GameEntity[] entities2 = gameAsyncRequestsGroup.GetEntities();
			foreach (GameEntity request2 in entities2)
			{
				num += CheckGameRequest(request2);
			}
			num = Mathf.Clamp01((!(num2 > 0f)) ? 1f : (num / num2));
			if (num >= 1f)
			{
				RemoveAllRequests();
			}
			stateContext.sessionEntity.ReplacePendingAsyncRequests(num);
		}

		private float CheckUiRequest(UIEntity request)
		{
			float progress = request.asyncRequest.Request.progress;
			if (request.asyncRequest.Request.isDone && request.asyncRequest.callback != null)
			{
				request.asyncRequest.callback();
				request.asyncRequest.callback = null;
			}
			return progress;
		}

		private float CheckGameRequest(GameEntity request)
		{
			float progress = request.asyncRequest.Request.progress;
			if (request.asyncRequest.Request.isDone && request.asyncRequest.callback != null)
			{
				request.asyncRequest.callback();
				request.asyncRequest.callback = null;
			}
			return progress;
		}

		private void RemoveAllRequests()
		{
			UIEntity[] entities = uiAsyncRequestsGroup.GetEntities();
			foreach (UIEntity uIEntity in entities)
			{
				if (uIEntity.asyncRequest.Request.isDone)
				{
					uIEntity.RemoveAsyncRequest();
					uIEntity.isDestroyed = true;
				}
			}
			GameEntity[] entities2 = gameAsyncRequestsGroup.GetEntities();
			foreach (GameEntity gameEntity in entities2)
			{
				if (gameEntity.asyncRequest.Request.isDone)
				{
					gameEntity.RemoveAsyncRequest();
					gameEntity.isDestroyed = true;
				}
			}
		}
	}
}
