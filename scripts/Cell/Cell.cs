using System;
using Game.Inputs;
using Godot;

namespace Game.Cell;

public abstract partial class Cell : Area2D
{
	[Export] public float Size = 0;
	[Export] public float ScaleDivisor = 50.0f;
	[Export] public InputHandler Input;
	[Export] public float BaseVelocity = 2.0f;
	[Export] public float VelocityMultiplier = 1.0f;

	public override void _Ready()
	{
		base._Ready();

		UpdateScale();
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
		UpdateVelocity();
	}

	protected virtual void UpdateVelocity()
	{
		var minSize = 50f;
		var maxSize = 3000f;

		VelocityMultiplier = 1.0f + (MathF.Sqrt(Size) - MathF.Sqrt(minSize)) / (MathF.Sqrt(maxSize) - MathF.Sqrt(minSize));
	}
}
