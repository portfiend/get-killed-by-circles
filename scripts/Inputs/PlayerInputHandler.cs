using Godot;
using System;

namespace Game.Inputs;

public sealed partial class PlayerInputHandler : InputHandler
{
	public override void HandleMovementInputs(double delta)
	{
		HorizontalInput = Input.GetAxis("move_left", "move_right");
		VerticalInput = Input.GetAxis("move_up", "move_down");
	}
}
