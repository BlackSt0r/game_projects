extends Node

class_name GameManager

static var Instance: GameManager
static var gamePaused: bool = false
var _gameScene: PackedScene = load("res://Scenes/Game/game.tscn")
var _mainScene: PackedScene = load("res://Scenes/Main/main.tscn")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	Instance = self

static func load_main() -> void:
	Instance.get_tree().change_scene_to_packed(Instance._mainScene)

static func load_game() -> void:
	Instance.get_tree().change_scene_to_packed(Instance._gameScene)
