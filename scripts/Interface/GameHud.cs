using Game.Cell;
using Godot;
using System;

namespace Game.Interface;

public partial class GameHud : CanvasLayer
{
	[Export] public Label ScoreLabel;

	public void UpdateScore(float score)
	{
		var intScore = (int)score;
		ScoreLabel.Text = intScore.ToString();
	}
}
