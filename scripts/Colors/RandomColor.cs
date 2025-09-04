using Godot;
using System;
using System.Collections.Generic;

namespace Game.Colors;

public sealed partial class RandomColor : Sprite2D
{
	private static readonly List<Color> POSSIBLE_COLORS = new()
	{
		Color.FromHtml("#f06458"),
		Color.FromHtml("#f9a358"),
		Color.FromHtml("#f9e358"),
		Color.FromHtml("#c4ea36"),
		Color.FromHtml("#77da44"),
		Color.FromHtml("#57de9e"),
		Color.FromHtml("#3fe3df"),
		Color.FromHtml("#5f93de"),
		Color.FromHtml("#ab6add"),
		Color.FromHtml("#d76add"),
		Color.FromHtml("#f44995"),
	};

	private static Random rng = new();

	public override void _Ready()
	{
		base._Ready();

		var color = POSSIBLE_COLORS[rng.Next(POSSIBLE_COLORS.Count)];
		Modulate = color;
	}
}
