using Game.Cell;
using Game.Interface;
using Godot;
using System;

namespace Game;

public partial class Game : Node2D
{
	[Export] public PlayerCell Player;
	[Export] public GameHud Hud;
	[Export] public GameOverHud GameOverHud;
	[Export] public Node GameObjects;

	private float PlayerScore => Player.Size - Player.BASE_SIZE;
	private float WIN_THRESHOLD = 5000.0f;
	private bool _gameOver = false;

	public override void _Ready()
	{
		base._Ready();

		Player.ScoreUpdated += _ =>
		{
			Hud.UpdateScore(PlayerScore);
			if (PlayerScore > WIN_THRESHOLD)
				GameOverHud.GameOver(PlayerScore, wonGame: true);	
		};

		Player.Died += OnPlayerDied;

		Hud.UpdateScore(PlayerScore);
	}

	private void OnPlayerDied()
	{
		GameObjects.QueueFree();
		GameOverHud.GameOver(PlayerScore, wonGame: false);
		_gameOver = true;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (Input.IsActionPressed("ui_accept") && _gameOver)
			GetTree().ReloadCurrentScene();
	}
}
