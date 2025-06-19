extends Area2D
class_name Alien
@export var _collisionShape: CollisionShape2D
@export var _sprite: AnimatedSprite2D
var _incrementSize: float = 0.04
var _walk: Vector2 = Vector2(0, 20)
var _alreadyHit: bool = false

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	Moving(delta)

func Moving(delta: float) -> void:
	if get_viewport_rect().end.y < position.y:
		SignalManager.EmitSignalLose("YOU LOSE :(")
		Chau()
	position += _walk * delta
	_sprite.scale *= 1+ delta * _incrementSize
	_collisionShape.scale *= 1 + delta * _incrementSize

func Chau() -> void:
	call_deferred("queue_free")

func AlienPause() -> void:
	set_process(not GameManager.gamePaused)

func Pause(_text: String) -> void:
	set_process(false)

func DesactivateCollision(_txt: String) -> void:
	_collisionShape.disabled = false
