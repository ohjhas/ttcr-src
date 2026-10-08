using Entitas;
using UnityEngine;

public sealed class InputEntity : Entity
{
	private static readonly CollisionTriggerComponent collisionTriggerComponent = new CollisionTriggerComponent();

	private static readonly DeadFeedbackComponent deadFeedbackComponent = new DeadFeedbackComponent();

	private static readonly GoBackComponent goBackComponent = new GoBackComponent();

	private static readonly InputComponent inputComponent = new InputComponent();

	private static readonly InputJumpComponent inputJumpComponent = new InputJumpComponent();

	private static readonly InputJumpPerformedComponent inputJumpPerformedComponent = new InputJumpPerformedComponent();

	private static readonly InputSlideComponent inputSlideComponent = new InputSlideComponent();

	private static readonly InputSlideStartComponent inputSlideStartComponent = new InputSlideStartComponent();

	private static readonly InputSlideStopComponent inputSlideStopComponent = new InputSlideStopComponent();

	private static readonly LandingComponent landingComponent = new LandingComponent();

	private static readonly LevelEndAnimationFinishedComponent levelEndAnimationFinishedComponent = new LevelEndAnimationFinishedComponent();

	private static readonly MusicToggledComponent musicToggledComponent = new MusicToggledComponent();

	private static readonly PlayerLostComponent playerLostComponent = new PlayerLostComponent();

	private static readonly SoundsToggledComponent soundsToggledComponent = new SoundsToggledComponent();

	private static readonly TouchDownComponent touchDownComponent = new TouchDownComponent();

	public BufferComponent buffer
	{
		get
		{
			return (BufferComponent)GetComponent(0);
		}
	}

	public bool hasBuffer
	{
		get
		{
			return HasComponent(0);
		}
	}

	public ChangeViewSizeComponent changeViewSize
	{
		get
		{
			return (ChangeViewSizeComponent)GetComponent(1);
		}
	}

	public bool hasChangeViewSize
	{
		get
		{
			return HasComponent(1);
		}
	}

	public CollisionComponent collision
	{
		get
		{
			return (CollisionComponent)GetComponent(2);
		}
	}

	public bool hasCollision
	{
		get
		{
			return HasComponent(2);
		}
	}

	public CollisionFeedbackComponent collisionFeedback
	{
		get
		{
			return (CollisionFeedbackComponent)GetComponent(3);
		}
	}

	public bool hasCollisionFeedback
	{
		get
		{
			return HasComponent(3);
		}
	}

	public bool isCollisionTrigger
	{
		get
		{
			return HasComponent(4);
		}
		set
		{
			if (value != isCollisionTrigger)
			{
				if (value)
				{
					AddComponent(4, collisionTriggerComponent);
				}
				else
				{
					RemoveComponent(4);
				}
			}
		}
	}

	public CreateParticlesComponent createParticles
	{
		get
		{
			return (CreateParticlesComponent)GetComponent(5);
		}
	}

	public bool hasCreateParticles
	{
		get
		{
			return HasComponent(5);
		}
	}

	public bool isDeadFeedback
	{
		get
		{
			return HasComponent(6);
		}
		set
		{
			if (value != isDeadFeedback)
			{
				if (value)
				{
					AddComponent(6, deadFeedbackComponent);
				}
				else
				{
					RemoveComponent(6);
				}
			}
		}
	}

	public DestroyOldSkinComponent destroyOldSkin
	{
		get
		{
			return (DestroyOldSkinComponent)GetComponent(7);
		}
	}

	public bool hasDestroyOldSkin
	{
		get
		{
			return HasComponent(7);
		}
	}

	public bool isGoBack
	{
		get
		{
			return HasComponent(8);
		}
		set
		{
			if (value != isGoBack)
			{
				if (value)
				{
					AddComponent(8, goBackComponent);
				}
				else
				{
					RemoveComponent(8);
				}
			}
		}
	}

	public bool isInput
	{
		get
		{
			return HasComponent(9);
		}
		set
		{
			if (value != isInput)
			{
				if (value)
				{
					AddComponent(9, inputComponent);
				}
				else
				{
					RemoveComponent(9);
				}
			}
		}
	}

	public bool isInputJump
	{
		get
		{
			return HasComponent(10);
		}
		set
		{
			if (value != isInputJump)
			{
				if (value)
				{
					AddComponent(10, inputJumpComponent);
				}
				else
				{
					RemoveComponent(10);
				}
			}
		}
	}

	public bool isInputJumpPerformed
	{
		get
		{
			return HasComponent(11);
		}
		set
		{
			if (value != isInputJumpPerformed)
			{
				if (value)
				{
					AddComponent(11, inputJumpPerformedComponent);
				}
				else
				{
					RemoveComponent(11);
				}
			}
		}
	}

	public bool isInputSlide
	{
		get
		{
			return HasComponent(12);
		}
		set
		{
			if (value != isInputSlide)
			{
				if (value)
				{
					AddComponent(12, inputSlideComponent);
				}
				else
				{
					RemoveComponent(12);
				}
			}
		}
	}

	public bool isInputSlideStart
	{
		get
		{
			return HasComponent(13);
		}
		set
		{
			if (value != isInputSlideStart)
			{
				if (value)
				{
					AddComponent(13, inputSlideStartComponent);
				}
				else
				{
					RemoveComponent(13);
				}
			}
		}
	}

	public bool isInputSlideStop
	{
		get
		{
			return HasComponent(14);
		}
		set
		{
			if (value != isInputSlideStop)
			{
				if (value)
				{
					AddComponent(14, inputSlideStopComponent);
				}
				else
				{
					RemoveComponent(14);
				}
			}
		}
	}

	public bool isLanding
	{
		get
		{
			return HasComponent(15);
		}
		set
		{
			if (value != isLanding)
			{
				if (value)
				{
					AddComponent(15, landingComponent);
				}
				else
				{
					RemoveComponent(15);
				}
			}
		}
	}

	public bool isLevelEndAnimationFinished
	{
		get
		{
			return HasComponent(16);
		}
		set
		{
			if (value != isLevelEndAnimationFinished)
			{
				if (value)
				{
					AddComponent(16, levelEndAnimationFinishedComponent);
				}
				else
				{
					RemoveComponent(16);
				}
			}
		}
	}

	public bool isMusicToggled
	{
		get
		{
			return HasComponent(17);
		}
		set
		{
			if (value != isMusicToggled)
			{
				if (value)
				{
					AddComponent(17, musicToggledComponent);
				}
				else
				{
					RemoveComponent(17);
				}
			}
		}
	}

	public bool isPlayerLost
	{
		get
		{
			return HasComponent(18);
		}
		set
		{
			if (value != isPlayerLost)
			{
				if (value)
				{
					AddComponent(18, playerLostComponent);
				}
				else
				{
					RemoveComponent(18);
				}
			}
		}
	}

	public bool isSoundsToggled
	{
		get
		{
			return HasComponent(19);
		}
		set
		{
			if (value != isSoundsToggled)
			{
				if (value)
				{
					AddComponent(19, soundsToggledComponent);
				}
				else
				{
					RemoveComponent(19);
				}
			}
		}
	}

	public bool isTouchDown
	{
		get
		{
			return HasComponent(20);
		}
		set
		{
			if (value != isTouchDown)
			{
				if (value)
				{
					AddComponent(20, touchDownComponent);
				}
				else
				{
					RemoveComponent(20);
				}
			}
		}
	}

	public void AddBuffer(float newDuration)
	{
		int index = 0;
		BufferComponent bufferComponent = CreateComponent<BufferComponent>(index);
		bufferComponent.Duration = newDuration;
		AddComponent(index, bufferComponent);
	}

	public void ReplaceBuffer(float newDuration)
	{
		int index = 0;
		BufferComponent bufferComponent = CreateComponent<BufferComponent>(index);
		bufferComponent.Duration = newDuration;
		ReplaceComponent(index, bufferComponent);
	}

	public void RemoveBuffer()
	{
		RemoveComponent(0);
	}

	public void AddChangeViewSize(float newSize)
	{
		int index = 1;
		ChangeViewSizeComponent changeViewSizeComponent = CreateComponent<ChangeViewSizeComponent>(index);
		changeViewSizeComponent.Size = newSize;
		AddComponent(index, changeViewSizeComponent);
	}

	public void ReplaceChangeViewSize(float newSize)
	{
		int index = 1;
		ChangeViewSizeComponent changeViewSizeComponent = CreateComponent<ChangeViewSizeComponent>(index);
		changeViewSizeComponent.Size = newSize;
		ReplaceComponent(index, changeViewSizeComponent);
	}

	public void RemoveChangeViewSize()
	{
		RemoveComponent(1);
	}

	public void AddCollision(IEntity newFrom, IEntity newTo)
	{
		int index = 2;
		CollisionComponent collisionComponent = CreateComponent<CollisionComponent>(index);
		collisionComponent.from = newFrom;
		collisionComponent.to = newTo;
		AddComponent(index, collisionComponent);
	}

	public void ReplaceCollision(IEntity newFrom, IEntity newTo)
	{
		int index = 2;
		CollisionComponent collisionComponent = CreateComponent<CollisionComponent>(index);
		collisionComponent.from = newFrom;
		collisionComponent.to = newTo;
		ReplaceComponent(index, collisionComponent);
	}

	public void RemoveCollision()
	{
		RemoveComponent(2);
	}

	public void AddCollisionFeedback(bool newTouchPointIsLow)
	{
		int index = 3;
		CollisionFeedbackComponent collisionFeedbackComponent = CreateComponent<CollisionFeedbackComponent>(index);
		collisionFeedbackComponent.TouchPointIsLow = newTouchPointIsLow;
		AddComponent(index, collisionFeedbackComponent);
	}

	public void ReplaceCollisionFeedback(bool newTouchPointIsLow)
	{
		int index = 3;
		CollisionFeedbackComponent collisionFeedbackComponent = CreateComponent<CollisionFeedbackComponent>(index);
		collisionFeedbackComponent.TouchPointIsLow = newTouchPointIsLow;
		ReplaceComponent(index, collisionFeedbackComponent);
	}

	public void RemoveCollisionFeedback()
	{
		RemoveComponent(3);
	}

	public void AddCreateParticles(Vector3 newPosition)
	{
		int index = 5;
		CreateParticlesComponent createParticlesComponent = CreateComponent<CreateParticlesComponent>(index);
		createParticlesComponent.Position = newPosition;
		AddComponent(index, createParticlesComponent);
	}

	public void ReplaceCreateParticles(Vector3 newPosition)
	{
		int index = 5;
		CreateParticlesComponent createParticlesComponent = CreateComponent<CreateParticlesComponent>(index);
		createParticlesComponent.Position = newPosition;
		ReplaceComponent(index, createParticlesComponent);
	}

	public void RemoveCreateParticles()
	{
		RemoveComponent(5);
	}

	public void AddDestroyOldSkin(string newOldSuffix)
	{
		int index = 7;
		DestroyOldSkinComponent destroyOldSkinComponent = CreateComponent<DestroyOldSkinComponent>(index);
		destroyOldSkinComponent.oldSuffix = newOldSuffix;
		AddComponent(index, destroyOldSkinComponent);
	}

	public void ReplaceDestroyOldSkin(string newOldSuffix)
	{
		int index = 7;
		DestroyOldSkinComponent destroyOldSkinComponent = CreateComponent<DestroyOldSkinComponent>(index);
		destroyOldSkinComponent.oldSuffix = newOldSuffix;
		ReplaceComponent(index, destroyOldSkinComponent);
	}

	public void RemoveDestroyOldSkin()
	{
		RemoveComponent(7);
	}
}
