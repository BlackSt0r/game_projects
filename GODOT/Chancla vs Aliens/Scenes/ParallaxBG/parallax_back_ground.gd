extends Parallax2D

@export var _srcTexture: Texture2D
@export var _sprite: Sprite2D
@export var _speedScale: float
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	ScrollBackGround()

func ScrollBackGround() -> void:
	autoscroll = Vector2(-_speedScale * 120.0, 0)
	var scaleFactor = get_viewport_rect().size.y / _srcTexture.get_height()
	_sprite.texture = _srcTexture
	_sprite.scale = Vector2(scaleFactor, scaleFactor)
	repeat_size = Vector2(_srcTexture.get_width() * scaleFactor, 0)
