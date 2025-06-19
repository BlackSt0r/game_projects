using Godot;
using System;

public partial class SignalManager : Node
{
	public static SignalManager Instance { get; private set; }
	[Signal] public delegate void MousePositionEventHandler(Vector2 vec);
	[Signal] public delegate void ChanclaExitedEventHandler();
	[Signal] public delegate void NewChanclaEventHandler();
	[Signal] public delegate void HitEnemyEventHandler();
	[Signal] public delegate void GamePauseEventHandler();
	[Signal] public delegate void WinEventHandler(string text);
	[Signal] public delegate void LoseEventHandler(string text);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	public static void EmitSignalMousePosition(Vector2 pos)
	{
		Instance.EmitSignal(SignalName.MousePosition, pos);
	}

	public static void EmitSignalHit()
	{
		Instance.EmitSignal(SignalName.ChanclaExited);
	}

	public static void EmitSignalHitEnemy()
	{
		Instance.EmitSignal(SignalName.HitEnemy);
	}

	public static void EmitSignalNewChancla()
	{
		Instance.EmitSignal(SignalName.NewChancla);
	}

	public static void EmitSignalGamePause()
	{
		Instance.EmitSignal(SignalName.GamePause);
	}

	public static void EmitSignalWin()
	{
		Instance.EmitSignal(SignalName.Win, "YOU WIN!! :)");
	}

	public static void EmitSignalLose()
	{
		Instance.EmitSignal(SignalName.Lose, "YOU LOSE :(");
	}
}
