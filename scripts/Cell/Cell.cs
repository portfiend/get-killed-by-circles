using Godot;

namespace Game.Cell;

public abstract partial class Cell : Area2D
{
	[Export] public float Size = 0;
	[Export] public float ScaleDivisor = 100.0f;

	public override void _Ready()
	{
		base._Ready();

		BodyEntered += OnBodyEntered;

		UpdateScale();
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is not Cell otherCell)
			return;

		AttemptEat(otherCell);
	}

	protected void UpdateScale()
	{
		var newSize = Size / ScaleDivisor;
		Scale = Vector2.One + new Vector2(newSize, newSize);
	}

	protected virtual void AttemptEat(Cell otherCell)
	{
		if (Size < otherCell.Size)
			Die();
	}

	private void Die()
	{
		QueueFree();
	}
}
