using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class PlayerSlideSystem : ReactiveSystem<InputEntity>
{
	private readonly GameContext gameContext;

	private readonly InputContext inputContext;

	public PlayerSlideSystem(Contexts contexts)
		: base((IContext<InputEntity>)contexts.input)
	{
		gameContext = contexts.game;
		inputContext = contexts.input;
	}

	protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
	{
		return context.CreateCollector(InputMatcher.InputSlide);
	}

	protected override bool Filter(InputEntity entity)
	{
		return entity.isInputSlide && !gameContext.playerEntity.isAngryRun;
	}

	protected override void Execute(List<InputEntity> entities)
	{
		GameEntity playerEntity = gameContext.playerEntity;
		if (!playerEntity.isDead && gameContext.playerEntity.hasView)
		{
			GameObject gameObject = playerEntity.view.gameObject;
			Animator component = gameObject.GetComponent<Animator>();
			BoxCollider2D component2 = gameObject.GetComponent<BoxCollider2D>();
			Rigidbody2D component3 = gameObject.GetComponent<Rigidbody2D>();
			playerEntity.isSliding = true;
			InputEntity inputEntity = inputContext.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isInputSlideStart = !component.GetBool("Slide");
			component.SetBool("Slide", true);
			Vector2 colliderSlide = SettingsService.SettingsSO.ColliderSlide;
			component2.offset = new Vector2(component2.offset.x, colliderSlide.x);
			component2.size = new Vector2(component2.size.x, colliderSlide.y);
			if (inputEntity.isInputSlideStart && playerEntity.hasJumping && playerEntity.jumping.Quantity <= SettingsService.SettingsSO.JumpsQuantity)
			{
				playerEntity.ReplaceJumping(SettingsService.SettingsSO.JumpsQuantity + 1);
				float jumpForce = SettingsService.SettingsSO.JumpForce;
				component3.velocity = Vector2.zero;
				component3.AddForce(-gameObject.transform.up * jumpForce, ForceMode2D.Impulse);
			}
		}
	}
}
