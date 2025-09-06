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
	private float WIN_THRESHOLD = 8000.0f;
	private bool _gameOver = false;

	public override void _Ready()
	{
		base._Ready();

		Player.ScoreUpdated += _ =>
		{
			Hud.UpdateScore(PlayerScore);
			if (PlayerScore > WIN_THRESHOLD)
				EndGame(true);
		};

		Player.Died += () => { EndGame(false); };

		Hud.UpdateScore(PlayerScore);
	}

	private void EndGame(bool wonGame = false)
	{
		GameObjects.QueueFree();
		GameOverHud.GameOver(PlayerScore, wonGame);
		_gameOver = true;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (Input.IsActionPressed("ui_accept") && _gameOver)
			GetTree().ReloadCurrentScene();
	}
}
