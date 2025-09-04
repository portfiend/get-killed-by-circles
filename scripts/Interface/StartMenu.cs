using Godot;
using System;

namespace Game.Interface;

public partial class StartMenu : Node2D
{
	[Export] private PackedScene _gameScene;

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (Input.IsActionPressed("ui_accept"))
			GetTree().ChangeSceneToPacked(_gameScene);
	}
}
