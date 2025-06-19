using Godot;
using System;

public partial class WinLoseScene : Control
{
	[Export] private Label _LabelWinLose;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.Win += LabelText;
		SignalManager.Instance.Lose += LabelText;
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.Win -= LabelText;
		SignalManager.Instance.Lose -= LabelText;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void LabelText(string text)
	{
		_LabelWinLose.Text = text;
	}
}
