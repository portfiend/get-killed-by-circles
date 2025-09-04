using Godot;
using System;

namespace Game.Cell;

public sealed partial class PlayerCell : Cell
{
	[Export] public float BASE_SIZE = 50.0f;
	[Export] private float GROWTH_DIVISOR = 10.0f;
	private Vector2 _screenSize;
	
	[Signal]
	public delegate void ScoreUpdatedEventHandler(float score);

	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
		Size = BASE_SIZE;

		AreaEntered += OnAreaEntered;
		base._Ready();
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is not EnemyCell otherCell)
			return;

		AttemptEat(otherCell);
	}

	protected override void HandleMovement(double delta)
	{
		base.HandleMovement(delta);
		Position = Position.Clamp(Vector2.Zero, _screenSize);
	}

	protected override void UpdateScale()
	{
		base.UpdateScale();
		EmitSignal(SignalName.ScoreUpdated, Size - BASE_SIZE);
	}

	private void AttemptEat(EnemyCell otherCell)
	{
		if (Size < otherCell.Size)
			Die();
		if (Size > otherCell.Size)
			Eat(otherCell);
	}

	private void Die()
	{
		QueueFree();
	}

	private void Eat(EnemyCell otherCell)
	{
		otherCell.QueueFree();

		Size += otherCell.Size / GROWTH_DIVISOR;
		UpdateScale();
	}
}
