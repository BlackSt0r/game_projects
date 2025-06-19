using Godot;
using System;
using System.Threading.Tasks;

public partial class Main : Control
{
	[Export] private AudioStreamPlayer _audio;
	private bool _spacePressed = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override async void _Process(double delta)
	{
		if (Input.IsActionJustPressed("space") && !_spacePressed)
		{
			_spacePressed = true;
			_audio.Play();
			await ToSignal(_audio, "finished");
			GameManager.LoadGame();	
		}
    }
}
