using Entitas;

public sealed class InputMatcher
{
	private static IMatcher<InputEntity> _matcherBuffer;

	private static IMatcher<InputEntity> _matcherChangeViewSize;

	private static IMatcher<InputEntity> _matcherCollision;

	private static IMatcher<InputEntity> _matcherCollisionFeedback;

	private static IMatcher<InputEntity> _matcherCollisionTrigger;

	private static IMatcher<InputEntity> _matcherCreateParticles;

	private static IMatcher<InputEntity> _matcherDeadFeedback;

	private static IMatcher<InputEntity> _matcherDestroyOldSkin;

	private static IMatcher<InputEntity> _matcherGoBack;

	private static IMatcher<InputEntity> _matcherInput;

	private static IMatcher<InputEntity> _matcherInputJump;

	private static IMatcher<InputEntity> _matcherInputJumpPerformed;

	private static IMatcher<InputEntity> _matcherInputSlide;

	private static IMatcher<InputEntity> _matcherInputSlideStart;

	private static IMatcher<InputEntity> _matcherInputSlideStop;

	private static IMatcher<InputEntity> _matcherLanding;

	private static IMatcher<InputEntity> _matcherLevelEndAnimationFinished;

	private static IMatcher<InputEntity> _matcherMusicToggled;

	private static IMatcher<InputEntity> _matcherPlayerLost;

	private static IMatcher<InputEntity> _matcherSoundsToggled;

	private static IMatcher<InputEntity> _matcherTouchDown;

	public static IMatcher<InputEntity> Buffer
	{
		get
		{
			if (_matcherBuffer == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(default(int));
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherBuffer = matcher;
			}
			return _matcherBuffer;
		}
	}

	public static IMatcher<InputEntity> ChangeViewSize
	{
		get
		{
			if (_matcherChangeViewSize == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(1);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherChangeViewSize = matcher;
			}
			return _matcherChangeViewSize;
		}
	}

	public static IMatcher<InputEntity> Collision
	{
		get
		{
			if (_matcherCollision == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(2);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherCollision = matcher;
			}
			return _matcherCollision;
		}
	}

	public static IMatcher<InputEntity> CollisionFeedback
	{
		get
		{
			if (_matcherCollisionFeedback == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(3);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherCollisionFeedback = matcher;
			}
			return _matcherCollisionFeedback;
		}
	}

	public static IMatcher<InputEntity> CollisionTrigger
	{
		get
		{
			if (_matcherCollisionTrigger == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(4);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherCollisionTrigger = matcher;
			}
			return _matcherCollisionTrigger;
		}
	}

	public static IMatcher<InputEntity> CreateParticles
	{
		get
		{
			if (_matcherCreateParticles == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(5);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherCreateParticles = matcher;
			}
			return _matcherCreateParticles;
		}
	}

	public static IMatcher<InputEntity> DeadFeedback
	{
		get
		{
			if (_matcherDeadFeedback == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(6);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherDeadFeedback = matcher;
			}
			return _matcherDeadFeedback;
		}
	}

	public static IMatcher<InputEntity> DestroyOldSkin
	{
		get
		{
			if (_matcherDestroyOldSkin == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(7);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherDestroyOldSkin = matcher;
			}
			return _matcherDestroyOldSkin;
		}
	}

	public static IMatcher<InputEntity> GoBack
	{
		get
		{
			if (_matcherGoBack == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(8);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherGoBack = matcher;
			}
			return _matcherGoBack;
		}
	}

	public static IMatcher<InputEntity> Input
	{
		get
		{
			if (_matcherInput == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(9);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherInput = matcher;
			}
			return _matcherInput;
		}
	}

	public static IMatcher<InputEntity> InputJump
	{
		get
		{
			if (_matcherInputJump == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(10);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherInputJump = matcher;
			}
			return _matcherInputJump;
		}
	}

	public static IMatcher<InputEntity> InputJumpPerformed
	{
		get
		{
			if (_matcherInputJumpPerformed == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(11);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherInputJumpPerformed = matcher;
			}
			return _matcherInputJumpPerformed;
		}
	}

	public static IMatcher<InputEntity> InputSlide
	{
		get
		{
			if (_matcherInputSlide == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(12);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherInputSlide = matcher;
			}
			return _matcherInputSlide;
		}
	}

	public static IMatcher<InputEntity> InputSlideStart
	{
		get
		{
			if (_matcherInputSlideStart == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(13);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherInputSlideStart = matcher;
			}
			return _matcherInputSlideStart;
		}
	}

	public static IMatcher<InputEntity> InputSlideStop
	{
		get
		{
			if (_matcherInputSlideStop == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(14);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherInputSlideStop = matcher;
			}
			return _matcherInputSlideStop;
		}
	}

	public static IMatcher<InputEntity> Landing
	{
		get
		{
			if (_matcherLanding == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(15);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherLanding = matcher;
			}
			return _matcherLanding;
		}
	}

	public static IMatcher<InputEntity> LevelEndAnimationFinished
	{
		get
		{
			if (_matcherLevelEndAnimationFinished == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(16);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherLevelEndAnimationFinished = matcher;
			}
			return _matcherLevelEndAnimationFinished;
		}
	}

	public static IMatcher<InputEntity> MusicToggled
	{
		get
		{
			if (_matcherMusicToggled == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(17);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherMusicToggled = matcher;
			}
			return _matcherMusicToggled;
		}
	}

	public static IMatcher<InputEntity> PlayerLost
	{
		get
		{
			if (_matcherPlayerLost == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(18);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherPlayerLost = matcher;
			}
			return _matcherPlayerLost;
		}
	}

	public static IMatcher<InputEntity> SoundsToggled
	{
		get
		{
			if (_matcherSoundsToggled == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(19);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherSoundsToggled = matcher;
			}
			return _matcherSoundsToggled;
		}
	}

	public static IMatcher<InputEntity> TouchDown
	{
		get
		{
			if (_matcherTouchDown == null)
			{
				Matcher<InputEntity> matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(20);
				matcher.componentNames = InputComponentsLookup.componentNames;
				_matcherTouchDown = matcher;
			}
			return _matcherTouchDown;
		}
	}

	public static IAllOfMatcher<InputEntity> AllOf(params int[] indices)
	{
		return Matcher<InputEntity>.AllOf(indices);
	}

	public static IAllOfMatcher<InputEntity> AllOf(params IMatcher<InputEntity>[] matchers)
	{
		return Matcher<InputEntity>.AllOf(matchers);
	}

	public static IAnyOfMatcher<InputEntity> AnyOf(params int[] indices)
	{
		return Matcher<InputEntity>.AnyOf(indices);
	}

	public static IAnyOfMatcher<InputEntity> AnyOf(params IMatcher<InputEntity>[] matchers)
	{
		return Matcher<InputEntity>.AnyOf(matchers);
	}
}
