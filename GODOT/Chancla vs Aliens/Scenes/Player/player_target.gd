extends Node2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("trow"):
		SignalManager.EmitSignalMousePosition(get_global_mouse_position())
	position = get_global_mouse_position()
