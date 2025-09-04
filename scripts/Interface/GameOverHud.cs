using Godot;
using System;

namespace Game.Interface;

public partial class GameOverHud : CanvasLayer
{
	[Export] public Label GameOverLabel;
	[Export] public Label FinalScoreLabel;
	[Export] public Button RestartButton;

	public void GameOver(float score, bool wonGame = false)
	{
		FinalScoreLabel.Text = $"Final Score: {(int)score}";

		if (wonGame)
			GameOverLabel.Text = "YOU WON";

		Visible = true;
	}
}
