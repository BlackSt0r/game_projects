using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class Brain : Alien
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		SignalManager.Instance.GamePause += AlienPause;
		SignalManager.Instance.Win += Pause;
		SignalManager.Instance.Lose += DesactivateCollision;
	}

	public override void _ExitTree()
	{
		AreaEntered -= OnAreaEntered;
		SignalManager.Instance.GamePause -= AlienPause;
		SignalManager.Instance.Win -= Pause;
		SignalManager.Instance.Lose -= DesactivateCollision;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}


	private void OnAreaEntered(Area2D area)
	{
		/* Sin _alreadyHit, lo que sucede es lo siguiente:
			Cuando la Chancla golpea a dos Aliens diferentes, estos ejecutan OnAreaEntered(area),
				pero ocurre un error porque el segundo Alien ejecuta el codigo cuando la Chancla esta en proceso de eliminarse (en el mismo o siguiente frame)
				por lo que, la chancla, no esta en un estado "válido".
				Entonces si la chancla sigue existiendo en un frame posterior y sigue tocando a Alien2, llama otra vez a la misma funcion y explota,
					porque la chancla no esta en un estado "válido".
			Lo que causa inconsistencia en la chancla, por esto al agregar la variable, el codigo se ejecuta una sola vez.
		*/

		if (!_alreadyHit && area is Chancla)
		{
			SignalManager.EmitSignalHitEnemy();
			_alreadyHit = true;     // Marcás que ya chocaste una vez
			Chau();
		}
	}
}
