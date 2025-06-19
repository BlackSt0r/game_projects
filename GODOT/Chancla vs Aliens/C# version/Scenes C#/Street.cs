using Godot;
using System;
using System.Threading.Tasks;

public partial class Street : Node2D
{
	[Export] private Area2D _collisionTop;
	[Export] private Area2D _collisionLeft;
	[Export] private Area2D _collisionRight;
	[Export] private Area2D _collisionBottom;
    private bool _chanclaDetectada = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_collisionLeft.AreaEntered += OnAreaEntered;
		_collisionRight.AreaEntered += OnAreaEntered;
		_collisionTop.AreaEntered += OnAreaEntered;
		_collisionBottom.AreaEntered += OnAreaEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
	}

	private void OnAreaEntered(Node2D area)
	{
		if(area is Chancla chancla){
			if(!chancla.chanclaExit){
				SignalManager.EmitSignalNoHit();
				chancla.chanclaExit = true;
			}
		}
	}
}
