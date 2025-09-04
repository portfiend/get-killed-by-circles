using Godot;
using System;

namespace Game.Cell;

public sealed partial class PlayerCell : Cell
{
	private Vector2 _screenSize;

	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
		Size = 10.0f;
		
		BodyEntered += OnBodyEntered;
		base._Ready();
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is not EnemyCell otherCell)
			return;

		AttemptEat(otherCell);
	}

	protected override void HandleMovement(double delta)
	{
		base.HandleMovement(delta);
		Position = Position.Clamp(Vector2.Zero, _screenSize);
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

		Size += otherCell.Size;
		UpdateScale();
	}
}
