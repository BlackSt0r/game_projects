using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	// When Chancla its gone and reaper a new one, its not with the Process(false)
	public static bool gamePaused = false;
	private PackedScene _gameScene = GD.Load<PackedScene>("res://Scenes/Game/game.tscn");
	private PackedScene _mainScene = GD.Load<PackedScene>("res://Scenes/Main/main.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.HitEnemy += Hit;
		Instance = this;
	}

	public static void LoadMain()
	{
		Instance.GetTree().ChangeSceneToPacked(Instance._mainScene);
	}

	public static void LoadGame()
	{
		Instance.GetTree().ChangeSceneToPacked(Instance._gameScene);
	}
}
