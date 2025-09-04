using Godot;
using System;

namespace Game.Timers;

public partial class RandomTimer : Node
{
    [Export] public double MinSpawnCooldown = 1.0f;
    [Export] public double MaxSpawnCooldown = 10.0f;

    public Timer Timer = new();
    private static Random rng = new();

    public override void _Ready()
    {
        base._Ready();

        AddChild(Timer);
        Timer.Timeout += InitTimer; 
        InitTimer();
    }

    public void InitTimer()
    {
        var range = MaxSpawnCooldown - MinSpawnCooldown;
        var waitTime = rng.NextDouble() * range + MinSpawnCooldown;

        Timer.Start(waitTime);
    }
}
