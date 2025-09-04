using Game.Cell;
using Game.Inputs;
using Game.Timers;
using Godot;
using System;

namespace Game.Spawner;

public partial class EnemySpawner : Node2D
{
	[Export] public int SpawnerCount = 8;
	[Export] public double MinSpawnCooldown = 1.0f;
	[Export] public double MaxSpawnCooldown = 10.0f;
	[Export] public Node2D _game;
	[Export] private PackedScene _enemyScene;

	private static Random rng = new();
	private Vector2 _screenSize;
	private const float SCREEN_OFFSET = 240.0f;

	public override void _Ready()
	{
		base._Ready();
		_screenSize = GetViewportRect().Size;

		for (int i = 0; i < SpawnerCount; i++)
		{
			var timer = new RandomTimer
			{
				MaxSpawnCooldown = MaxSpawnCooldown,
				MinSpawnCooldown = MinSpawnCooldown
			};

			timer.Timer.Timeout += SpawnEnemy;
			AddChild(timer);
		}
	}

	private void SpawnEnemy()
	{
		var enemy = (EnemyCell)_enemyScene.Instantiate();
		_game.AddChild(enemy);

		var handler = (EnemyInputHandler)enemy.Input;

		var dir = GetRandomDirection();
		handler.Direction = dir;

		var screenWidth = _screenSize.X;
		var screenHeight = _screenSize.Y;
		var pos = dir switch
		{
			MoveDirection.Up => new Vector2((float)(rng.NextDouble() * screenWidth), screenHeight + SCREEN_OFFSET),
			MoveDirection.Down => new Vector2((float)(rng.NextDouble() * screenWidth), 0 - SCREEN_OFFSET),
			MoveDirection.Left => new Vector2(screenWidth + SCREEN_OFFSET, (float)(rng.NextDouble() * screenHeight)),
			MoveDirection.Right => new Vector2(0 - SCREEN_OFFSET, (float)(rng.NextDouble() * screenHeight)),
			_ => Vector2.Zero
		};

		enemy.Position = pos;
		GD.Print($"Spawned a new enemy at {enemy.Position}");
	}

	private MoveDirection GetRandomDirection()
	{
		var values = Enum.GetValues(typeof(MoveDirection));
		return (MoveDirection)values.GetValue(rng.Next(values.Length));
	}
}
