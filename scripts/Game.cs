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
	private float WIN_THRESHOLD = 3000.0f;

	public override void _Ready()
	{
		base._Ready();

		Player.ScoreUpdated += _ => { Hud.UpdateScore(PlayerScore); };
		Player.Died += OnPlayerDied;

		Hud.UpdateScore(PlayerScore);
	}

	private void OnPlayerDied()
	{
		var finalScore = PlayerScore;
		GameObjects.QueueFree();

		GameOverHud.GameOver(PlayerScore, PlayerScore > WIN_THRESHOLD);
	}
}
