using Godot;
using System;
using System.Diagnostics;
public partial class Alien : Area2D
{
	[Export] private CollisionShape2D _collisionShape;
	[Export] private AnimatedSprite2D _sprite;
	private float _incrementSize = 0.04f;
	private Vector2 _walk = new Vector2(0, 20);
	protected bool _alreadyHit = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Process(double delta)
	{
		Moving(delta);
	}

	private void Moving(double delta)
	{
		if (GetViewportRect().End.Y < Position.Y)
		{
			SignalManager.EmitSignalLose();
			Chau();
		}
		Position += _walk * (float)delta;

		// Formula = 1 (para que no cambie tan drasticamente su escala, por si delta es 0)
		// 			 + delta (fotogramas por segundo)
		// 			 * _incrementSize (tasa de incremento en la escala)
		_sprite.Scale *= 1 + (float)delta * _incrementSize;
		_collisionShape.Scale *= 1 + (float)delta * _incrementSize;
	}

	protected void Chau()
	{
		CallDeferred("queue_free");
	}

	protected void AlienPause()
	{
		SetProcess(!GameManager.gamePaused);
	}

	protected void Pause(string text)
	{
		SetProcess(false);
	}

	protected void DesactivateCollision(string txt)
	{
		_collisionShape.Disabled = true;
	}
}