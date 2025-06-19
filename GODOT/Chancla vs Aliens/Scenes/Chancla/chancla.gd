extends Area2D

class_name Chancla
const SPEED := 400.0
var _decrement_size := 0.9
var _trow := false
var _direccion := Vector2.ZERO
@export var _collision: CollisionShape2D
@export var _sprite: Sprite2D
@export var _animation_exit: AnimationPlayer
var chancla_exit := false
var _ya_golpeo := false

func _ready():
	SignalManager.Instance.game_pause.connect(_pause_chancla)
	SignalManager.Instance.mouse_position.connect(_on_trow_chancla)
	SignalManager.Instance.chancla_exited.connect(_chancla_exited)
	SignalManager.Instance.hit_enemy.connect(_create_new_chancla)
	SignalManager.Instance.lose.connect(_pause)

func _exit_tree():
	SignalManager.Instance.game_pause.disconnect(_pause_chancla)
	SignalManager.Instance.mouse_position.disconnect(_on_trow_chancla)
	SignalManager.Instance.chancla_exited.disconnect(_chancla_exited)
	SignalManager.Instance.hit_enemy.disconnect(_create_new_chancla)
	SignalManager.Instance.lose.disconnect(_pause)

func _process(delta):
	if not GameManager.gamePaused and _trow:
		position += _direccion * SPEED * delta
		_collision.scale *= 1.0 - delta * _decrement_size
		_sprite.scale *= 1.0 - delta * _decrement_size

func _on_trow_chancla(pos: Vector2):
	if not _trow:
		_direccion = (pos - position).normalized()
	_trow = true

func _create_new_chancla():
	if not _ya_golpeo:
		_ya_golpeo = true
		SignalManager.EmitSignalNewChancla()
		call_deferred("queue_free")

func _chancla_exited() -> void:
	_animation_exit.play("fall")
	await _animation_exit.animation_finished
	_create_new_chancla()

func _pause_chancla():
	set_process(not GameManager.gamePaused)

func _pause(_text: String):
	set_process(false)
