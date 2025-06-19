using Godot;
using System;
using System.Threading.Tasks;

public partial class Dientitos : Alien
{
	private int _vida = 4;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += HitEnemy;
		SignalManager.Instance.GamePause += AlienPause;
		SignalManager.Instance.Win += Pause;
		SignalManager.Instance.Lose += DesactivateCollision;
	}

	public override void _ExitTree()
	{
		AreaEntered -= HitEnemy;
		SignalManager.Instance.GamePause -= AlienPause;
		SignalManager.Instance.Win -= Pause;
		SignalManager.Instance.Lose -= DesactivateCollision;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	private void HitEnemy(Area2D body)
	{
		
		if (!_alreadyHit && body is Chancla)
		{
			_vida--;
			SignalManager.EmitSignalHitEnemy();
			if (_vida == 0)
			{
				_alreadyHit = true;
				Chau();
			}
		}
	}
}
