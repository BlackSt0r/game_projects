extends Control
class_name Main

@export var _audio: AudioStreamPlayer
var _spacePressed: bool = false

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("space") && (not _spacePressed):
		_spacePressed = true
		_audio.play()
		await _audio.finished
		GameManager.load_game()
