using Godot;
using System;

namespace Game.Cell;

public sealed partial class EnemyCell : Cell
{
	[Export] private float MAX_SIZE = 2000.0f;

	private static Random rng = new();

	public override void _Ready()
	{
		Size = (float) rng.NextDouble() * MAX_SIZE;
		base._Ready();
	}
}
