using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins;
using Entitas;
using UnityEngine;

public sealed class ChangeViewSizeSystem : ReactiveSystem<InputEntity>, IInitializeSystem, ISystem
{
	private const float cameraDefaultSize = 6f;

	private const float transitionDuration = 1f;

	private readonly GameContext gameContext;

	public ChangeViewSizeSystem(Contexts contexts)
		: base((IContext<InputEntity>)contexts.input)
	{
		gameContext = contexts.game;
	}

	public void Initialize()
	{
		int camerasize = gameContext.worldEntity.missionData.MissionData.Camerasize;
		Camera main = Camera.main;
		Vector3 localPosition = new Vector3((float)camerasize - 6f, (float)camerasize - 6f, main.transform.position.z);
		main.transform.localPosition = localPosition;
		main.orthographicSize = camerasize;
	}

	protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
	{
		return context.CreateCollector(InputMatcher.ChangeViewSize);
	}

	protected override bool Filter(InputEntity entity)
	{
		return entity.hasChangeViewSize;
	}

	protected override void Execute(List<InputEntity> entities)
	{
		foreach (InputEntity entity in entities)
		{
			float size = entity.changeViewSize.Size;
			ChangeSize(size);
		}
	}

	private void ChangeSize(float size)
	{
		Camera main = Camera.main;
		Vector3 vector = new Vector3(size - 6f, size - 6f, main.transform.position.z);
		main.transform.DOLocalMove(vector, 1f);
		DOTween.To(new Vector2Plugin(), () => gameContext.ingameBgEntity.ingameBg.Bg.cameraPos, delegate(Vector2 pos)
		{
			gameContext.ingameBgEntity.ingameBg.Bg.cameraPos = pos;
		}, vector, 1f);
		main.DOOrthoSize(size, 1f);
	}
}
