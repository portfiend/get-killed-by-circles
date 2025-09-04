using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;

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
	private const float RADIUS = 2.0f;
	private Vector2 _screenSize;
	private const float SCREEN_OFFSET = 240.0f;
	private float MaxOffset => Size / ScaleDivisor * RADIUS * 2 + SCREEN_OFFSET;

	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;

		var sizeRange = _sizeRanges[rng.RandiRange(0, _sizeRanges.Count - 1)];
		Size = rng.RandfRange(sizeRange.X, sizeRange.Y);

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
		VelocityMultiplier *= rng.RandfRange(0.5f, 1.1f);
    }
}
