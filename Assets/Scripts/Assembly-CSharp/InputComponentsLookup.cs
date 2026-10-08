using System;

public static class InputComponentsLookup
{
	public const int Buffer = 0;

	public const int ChangeViewSize = 1;

	public const int Collision = 2;

	public const int CollisionFeedback = 3;

	public const int CollisionTrigger = 4;

	public const int CreateParticles = 5;

	public const int DeadFeedback = 6;

	public const int DestroyOldSkin = 7;

	public const int GoBack = 8;

	public const int Input = 9;

	public const int InputJump = 10;

	public const int InputJumpPerformed = 11;

	public const int InputSlide = 12;

	public const int InputSlideStart = 13;

	public const int InputSlideStop = 14;

	public const int Landing = 15;

	public const int LevelEndAnimationFinished = 16;

	public const int MusicToggled = 17;

	public const int PlayerLost = 18;

	public const int SoundsToggled = 19;

	public const int TouchDown = 20;

	public const int TotalComponents = 21;

	public static readonly string[] componentNames = new string[21]
	{
		"Buffer", "ChangeViewSize", "Collision", "CollisionFeedback", "CollisionTrigger", "CreateParticles", "DeadFeedback", "DestroyOldSkin", "GoBack", "Input",
		"InputJump", "InputJumpPerformed", "InputSlide", "InputSlideStart", "InputSlideStop", "Landing", "LevelEndAnimationFinished", "MusicToggled", "PlayerLost", "SoundsToggled",
		"TouchDown"
	};

	public static readonly Type[] componentTypes = new Type[21]
	{
		typeof(BufferComponent),
		typeof(ChangeViewSizeComponent),
		typeof(CollisionComponent),
		typeof(CollisionFeedbackComponent),
		typeof(CollisionTriggerComponent),
		typeof(CreateParticlesComponent),
		typeof(DeadFeedbackComponent),
		typeof(DestroyOldSkinComponent),
		typeof(GoBackComponent),
		typeof(InputComponent),
		typeof(InputJumpComponent),
		typeof(InputJumpPerformedComponent),
		typeof(InputSlideComponent),
		typeof(InputSlideStartComponent),
		typeof(InputSlideStopComponent),
		typeof(LandingComponent),
		typeof(LevelEndAnimationFinishedComponent),
		typeof(MusicToggledComponent),
		typeof(PlayerLostComponent),
		typeof(SoundsToggledComponent),
		typeof(TouchDownComponent)
	};
}
