using Game.Cell;
using Game.Interface;
using Godot;
using System;

namespace Game;

public partial class Game : Node2D
{
	[Export] public PlayerCell Player;
	[Export] public GameHud Hud;

    public override void _Ready()
    {
        base._Ready();

        Player.ScoreUpdated += Hud.UpdateScore;
        Hud.UpdateScore(Player.Size - Player.BASE_SIZE);
	}
}
