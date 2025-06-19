using Godot;
using System;
using System.Threading.Tasks;


public partial class Chancla : Area2D
{
	private const float _SPEED = 400.0f;
	private float _decrementSize = 0.9f;
	private bool _trow = false;
	private Vector2 _direccion;
	[Export] private CollisionShape2D _collision;
	[Export] private Sprite2D _sprite;
	[Export] private AnimationPlayer _animationExit;
	public bool chanclaExit = false;
	private bool _yaGolpeo = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.GamePause += PauseChancla;
		SignalManager.Instance.MousePosition += OnTrowChancla;
		SignalManager.Instance.ChanclaExited += ChanclaExited;
		SignalManager.Instance.HitEnemy += CreateNewChancla;
		SignalManager.Instance.Lose += Pause;
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.GamePause -= PauseChancla;
		SignalManager.Instance.MousePosition -= OnTrowChancla;
		SignalManager.Instance.ChanclaExited -= ChanclaExited;
		SignalManager.Instance.HitEnemy -= CreateNewChancla;
		SignalManager.Instance.Lose -= Pause;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!GameManager.gamePaused && _trow)
		{
			// Move, in one direction, the Chancla Position 
			Position += _direccion * _SPEED * (float)delta;

			_collision.Scale *= 1 - (float)delta * _decrementSize;
			_sprite.Scale *= 1 - (float)delta * _decrementSize;
		}
	}

	private void OnTrowChancla(Vector2 pos)
	{
		if (!_trow)
		{
			_direccion = (pos - Position).Normalized();
		}
		_trow = true;
	}

	private void CreateNewChancla()
	{
		if (!_yaGolpeo)
		{
			_yaGolpeo = true;
			SignalManager.EmitSignalNewChancla();
			CallDeferred("queue_free");
		}
	}

	private async void ChanclaExited()
	{
		_animationExit.Play("fall");
		await ToSignal(_animationExit, "animation_finished");
		CreateNewChancla();
	}

	private void PauseChancla()
	{
		SetProcess(!GameManager.gamePaused);
	}

	private void Pause(string text)
	{
		SetProcess(false);
	}
}
