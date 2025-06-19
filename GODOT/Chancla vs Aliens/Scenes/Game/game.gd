extends Node2D
class_name Game
@export var _chancla_scene: PackedScene
@export var _brain_scene: PackedScene
@export var _dientitos_scene: PackedScene
@export var _marker_chancla: Marker2D
@export var _marker_alien_left: Marker2D
@export var _marker_alien_right: Marker2D
@export var _timer_alien_spawn: Timer
@export var _pause_menu: Control
@export var _timer_to_win: Timer
@export var _timer_label: Label
@export var _winlose: Control
@export var _hit_sound: AudioStreamPlayer

var _rng = RandomNumberGenerator.new()

func _ready():
	Input.mouse_mode = Input.MOUSE_MODE_HIDDEN
	_spawn_chancla()
	_timer_alien_spawn.timeout.connect(_spawn_alien)
	_timer_to_win.timeout.connect(_player_win)
	SignalManager.Instance.new_chancla.connect(_spawn_chancla)
	SignalManager.Instance.hit_enemy.connect(_hit)
	SignalManager.Instance.lose.connect(_player_lose)

func _exit_tree():
	SignalManager.Instance.new_chancla.disconnect(_spawn_chancla)
	SignalManager.Instance.hit_enemy.disconnect(_hit)
	SignalManager.Instance.lose.disconnect(_player_lose)

func _process(_delta):
	if not _winlose.visible and Input.is_action_just_pressed("pause"):
		_change_pause()

	if _winlose.visible and Input.is_action_just_pressed("pause"):
		GameManager.load_main()

	_timer_label.text = str(ceil(_timer_to_win.time_left))

func _spawn_alien():
	var alien: Alien = null
	match _rng.randi_range(0, 1):
		0:
			alien = _brain_scene.instantiate()
		1:
			alien = _dientitos_scene.instantiate()

	if alien:
		add_child(alien)
		alien.position = Vector2(_get_spawn_alien(), _marker_alien_left.position.y)

func _player_win():
	_timer_alien_spawn.stop()
	_winlose.visible = true
	SignalManager.EmitSignalWin("YOU WIN!! :)")

func _player_lose(_text: String):
	_timer_alien_spawn.stop()
	_timer_to_win.stop()
	_winlose.visible = true

func _get_spawn_alien() -> float:
	return randf_range(_marker_alien_left.position.x, _marker_alien_right.position.x)

func _spawn_chancla():
	var chancla = _chancla_scene.instantiate()
	call_deferred("add_child", chancla)
	chancla.position = Vector2(_marker_chancla.position.x, _marker_chancla.position.y)

func _change_pause():
	if not GameManager.gamePaused:
		_timer_alien_spawn.paused = true
		_timer_to_win.paused = true
		GameManager.gamePaused = true
		SignalManager.EmitSignalGamePause()
	else:
		GameManager.gamePaused = false
		_timer_alien_spawn.paused = false
		_timer_to_win.paused = false
		SignalManager.EmitSignalGamePause()
	_pause_menu.visible = GameManager.gamePaused

func _hit():
	_hit_sound.play()
