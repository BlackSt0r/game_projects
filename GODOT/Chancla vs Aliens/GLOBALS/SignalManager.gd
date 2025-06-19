extends Node

class_name SignalManager

static var Instance: SignalManager
signal mouse_position(vec: Vector2)
signal chancla_exited()
signal new_chancla()
signal hit_enemy()
signal game_pause()
signal win(txt: String)
signal lose(txt: String)


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	Instance = self

static func EmitSignalMousePosition(pos: Vector2):
	Instance.emit_signal("mouse_position", pos)

static func EmitSignalChanclaExited():
	Instance.emit_signal("chancla_exited")

static func EmitSignalNewChancla():
	Instance.emit_signal("new_chancla")

static func EmitSignalHitEnemy():
	Instance.emit_signal("hit_enemy")

static func EmitSignalGamePause():
	Instance.emit_signal("game_pause")

static func EmitSignalWin(txt: String):
	Instance.emit_signal("win", txt)

static func EmitSignalLose(txt: String):
	Instance.emit_signal("lose", txt)
