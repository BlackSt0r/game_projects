using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class Game : Node2D
{
	[Export] private PackedScene _chanclaScene;
	[Export] private PackedScene _brainScene;
	[Export] private PackedScene _dientitosScene;
	[Export] private Marker2D _markerChancla;
	[Export] private Marker2D _markerAlienLeft;
	[Export] private Marker2D _markerAlienRight;
	[Export] private Timer _timerAlienSpawn;
	[Export] private Control _pauseMenu;
	[Export] private Timer _timerToWin;     // HAY OTRA MANERA DE HACERLO
	[Export] private Label _timerLabel;
	[Export] private Control _winlose;
	[Export] private AudioStreamPlayer _hitSound;

	/* Para agarrar numeros aleatorios podemos hacer GD.RandRange(N, N); pero obtenes un float
		entonces hacemos la variable de instancia de RandomNumberGenerator.
	*/
	private RandomNumberGenerator _rng = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		SpawnChancla();
		_timerAlienSpawn.Timeout += SpawnAlien;
		_timerToWin.Timeout += PlayerWin;
		SignalManager.Instance.NewChancla += SpawnChancla;
		SignalManager.Instance.HitEnemy += Hit;
		SignalManager.Instance.Lose += PlayerLose;
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.NewChancla -= SpawnChancla;
		SignalManager.Instance.HitEnemy -= Hit;
		SignalManager.Instance.Lose -= PlayerLose;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!_winlose.IsVisibleInTree() && Input.IsActionJustPressed("pause"))
		{
			ChangePause();
		}

		if (_winlose.IsVisibleInTree() && Input.IsActionJustPressed("pause"))
		{
			GameManager.LoadMain();
		}

		_timerLabel.Text = $"{Mathf.CeilToInt(_timerToWin.TimeLeft)}";
	}

	private void SpawnAlien()
	{
		Alien alien = null;
		switch (_rng.RandiRange(0, 1))
		{
			case 0:
				alien = _brainScene.Instantiate<Brain>();
				break;
			case 1:
				alien = _dientitosScene.Instantiate<Dientitos>();
				break;
		}
		if (alien != null)
		{
			AddChild(alien);
			alien.Position = new Vector2(GetSpawnAlien(), _markerAlienLeft.Position.Y);
		}
	}

	private void PlayerWin()
	{
		_timerAlienSpawn.Stop();
		_winlose.Visible = true;
		SignalManager.EmitSignalWin();
	}

	private void PlayerLose(string text)
	{
		_timerAlienSpawn.Stop();
		_timerToWin.Stop();
		_winlose.Visible = true;
	}

	private float GetSpawnAlien()
	{
		return (float)GD.RandRange(_markerAlienLeft.Position.X, _markerAlienRight.Position.X);
	}

	private void SpawnChancla()
	{
		Chancla chancla = (Chancla)_chanclaScene.Instantiate();

		CallDeferred("add_child", chancla);
		chancla.Position = new Vector2(_markerChancla.Position.X, _markerChancla.Position.Y);
	}

	private void ChangePause()
	{
		if (!GameManager.gamePaused)
		{
			_timerAlienSpawn.Paused = true;
			_timerToWin.Paused = true;
			GameManager.gamePaused = true;
			SignalManager.EmitSignalGamePause();
		}
		else
		{
			GameManager.gamePaused = false;
			_timerAlienSpawn.Paused = false;
			_timerToWin.Paused = false;
			SignalManager.EmitSignalGamePause();
		}
		_pauseMenu.Visible = GameManager.gamePaused;
	}

	// Es mejor si utilizamos un AudioManager (pa el proximo)
	private void Hit()
	{
		_hitSound.Play();
	}
}