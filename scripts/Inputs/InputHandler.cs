using Godot;
using System;

namespace Game.Inputs;

public abstract partial class InputHandler : Node
{
    public float HorizontalInput { get; protected set; }
    public float VerticalInput { get; protected set; }

    public abstract void HandleMovementInputs(double delta);
}
