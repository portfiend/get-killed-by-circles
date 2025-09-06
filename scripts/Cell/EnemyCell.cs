using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Game.Cell;

public sealed partial class EnemyCell : Cell
{
	[Export] public bool RandomSize = true;

	private List<Vector2> _sizeRanges = new()
	{
		new(10.0f, 50.0f),
		new(50.0f, 200.0f),
		new(200.0f, 500.0f),
		new(500.0f, 1000.0f),
		new(1000.0f, 2500.0f),
		new(2500.0f, 5000.0f),
	};

	private static RandomNumberGenerator rng = new();
	private const float RADIUS = 2.0f;
	private const float SCREEN_OFFSET = 240.0f;
	private float MaxOffset => Size / ScaleDivisor * RADIUS * 2 + SCREEN_OFFSET;
	private Vector2 _screenSize;

	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;

		if (RandomSize)
		{
			var sizeRange = _sizeRanges[rng.RandiRange(0, _sizeRanges.Count - 1)];
			Size = rng.RandfRange(sizeRange.X, sizeRange.Y);
		}

		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		var x = Position.X;
		var y = Position.Y;
		var maxOffset = MaxOffset;

		if (x < -maxOffset || x > _screenSize.X + maxOffset
			|| y < -maxOffset || y > _screenSize.Y + maxOffset)
		{
			GD.Print($"Deleting enemy at {Position}");
			QueueFree();
		}
	}

	protected override void UpdateVelocity()
	{
		base.UpdateVelocity();
		VelocityMultiplier *= rng.RandfRange(0.5f, 1.5f);
	}
}
