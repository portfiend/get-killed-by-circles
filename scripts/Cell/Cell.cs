using Game.Inputs;
using Godot;

namespace Game.Cell;

public abstract partial class Cell : Area2D
{
	[Export] public float Size = 0;
	[Export] public float ScaleDivisor = 100.0f;
	[Export] public InputHandler Input;
	[Export] public float BaseVelocity = 2.0f;
	[Export] public float VelocityMultiplier = 1.0f;

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

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		HandleMovement(delta);
	}

	protected virtual void HandleMovement(double delta)
	{
		Input.HandleMovementInputs(delta);
		var speed = BaseVelocity * VelocityMultiplier;
		var velocity = new Vector2(Input.HorizontalInput * speed, Input.VerticalInput * speed);
		Position += velocity;
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
