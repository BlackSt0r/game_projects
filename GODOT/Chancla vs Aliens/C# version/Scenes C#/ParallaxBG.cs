using Godot;
using System;

public partial class ParallaxBG : Parallax2D
{
	[Export] private Texture2D _srcTexture;
	[Export] private Sprite2D _sprite;
	[Export] private float _speedScale;  // 0 - 1

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// To move the background
		ScrollBackGround();
	}

	private void ScrollBackGround()
	{
		Autoscroll = new Vector2(-_speedScale * 120.0f, 0);
		float scaleFactor = GetViewportRect().Size.Y / _srcTexture.GetHeight();

		_sprite.Texture = _srcTexture;
		_sprite.Scale = new Vector2(scaleFactor, scaleFactor);

		RepeatSize = new Vector2(_srcTexture.GetWidth() * scaleFactor, 0);
	}
}
