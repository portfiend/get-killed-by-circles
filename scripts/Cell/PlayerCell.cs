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
		
		base._Ready();
	}

	protected override void HandleMovement(double delta)
	{
		base.HandleMovement(delta);
		Position = Position.Clamp(Vector2.Zero, _screenSize);
    }

	protected override void AttemptEat(Cell otherCell)
	{
		base.AttemptEat(otherCell);

		if (Size > otherCell.Size)
			Eat(otherCell);
	}

	private void Eat(Cell otherCell)
	{
		Size += otherCell.Size;
		UpdateScale();
	}
}
