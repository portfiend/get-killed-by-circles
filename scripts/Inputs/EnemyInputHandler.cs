using Godot;
using System;

namespace Game.Inputs;

public sealed partial class EnemyInputHandler : InputHandler
{
	[Export] public MoveDirection Direction;

	public override void HandleMovementInputs(double delta)
	{
		HorizontalInput = Direction switch
		{
			MoveDirection.Left => -1.0f,
			MoveDirection.Right => 1.0f,
			_ => 0.0f,
		};

		VerticalInput = Direction switch
		{
			MoveDirection.Up => -1.0f,
			MoveDirection.Down => 1.0f,
			_ => 0.0f,
		};
	}
}

public enum MoveDirection
{
	Up,
	Down,
	Left,
	Right
}
