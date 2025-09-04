using Godot;
using System;
using System.Collections.Generic;

namespace Game.Cell;

public sealed partial class EnemyCell : Cell
{
	private List<Vector2> _sizeRanges = new()
	{
		new(10.0f, 50.0f),
		new(50.0f, 200.0f),
		new(200.0f, 700.0f),
		new(700.0f, 2500.0f),
	};

	private static RandomNumberGenerator rng = new();

	public override void _Ready()
	{
		var sizeRange = _sizeRanges[rng.RandiRange(0, _sizeRanges.Count - 1)];
		Size = rng.RandfRange(sizeRange.X, sizeRange.Y);

		base._Ready();
	}
}
