using Entitas;
using UnityEngine;

public sealed class PlayerJumpSystem : IExecuteSystem, ISystem
{
	private readonly GameContext _gameContext;

	private readonly InputContext _inputContext;

	private readonly IGroup<InputEntity> _jumpInputs;

	private readonly IGroup<InputEntity> _slideInputs;

	private readonly IGroup<GameEntity> _tutorialPerformedGroup;

	public PlayerJumpSystem(Contexts contexts)
	{
		_gameContext = contexts.game;
		_inputContext = contexts.input;
		_jumpInputs = _inputContext.GetGroup(InputMatcher.AnyOf(InputMatcher.InputJump, InputMatcher.TouchDown));
		_slideInputs = _inputContext.GetGroup(InputMatcher.AnyOf(InputMatcher.InputSlide, InputMatcher.InputSlideStart));
		_tutorialPerformedGroup = contexts.game.GetGroup(GameMatcher.TutorialPerformed);
	}

	public void Execute()
	{
		GameEntity playerEntity = _gameContext.playerEntity;
		if (playerEntity == null || playerEntity.isAngryRun || !playerEntity.hasView || playerEntity.isDead || (playerEntity.hasJumping && playerEntity.jumping.Quantity >= SettingsService.SettingsSO.JumpsQuantity) || _slideInputs.GetEntities().Length > 0 || IsPlayingSlideTutorial())
		{
			return;
		}
		InputEntity[] entities = _jumpInputs.GetEntities();
		foreach (InputEntity inputEntity in entities)
		{
			if (!inputEntity.hasBuffer)
			{
				break;
			}
			inputEntity.RemoveBuffer();
			InputPerformed();
			Jump(playerEntity);
		}
	}

	private bool IsPlayingSlideTutorial()
	{
		GameEntity playerEntity = _gameContext.playerEntity;
		return playerEntity.hasTutorialPlayStep && playerEntity.tutorialPlayStep.Step == TutorialStep.Slide && !HasSlideTutorialBeenPerformed();
	}

	private bool HasSlideTutorialBeenPerformed()
	{
		GameEntity[] entities = _tutorialPerformedGroup.GetEntities();
		foreach (GameEntity gameEntity in entities)
		{
			if (gameEntity.tutorialPerformed.Step == TutorialStep.Slide)
			{
				return true;
			}
		}
		return false;
	}

	private void InputPerformed()
	{
		InputEntity inputEntity = _inputContext.CreateEntity();
		inputEntity.isInput = true;
		inputEntity.isInputJumpPerformed = true;
	}

	private void Jump(GameEntity player)
	{
		GameObject gameObject = player.view.gameObject;
		int num = (player.hasJumping ? player.jumping.Quantity : 0);
		player.ReplaceJumping(++num);
		float jumpForce = SettingsService.SettingsSO.JumpForce;
		Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
		component.velocity = new Vector2(Mathf.Max(component.velocity.x * 0.37f, 0f), Mathf.Max(component.velocity.y * 0.37f, 0f));
		component.AddForce(gameObject.transform.up * jumpForce, ForceMode2D.Impulse);
		Animator component2 = gameObject.GetComponent<Animator>();
		component2.SetBool("Jump", true);
		component2.SetFloat("JumpAnim", Mathf.Round(Random.Range(0f, 1f)));
	}
}
