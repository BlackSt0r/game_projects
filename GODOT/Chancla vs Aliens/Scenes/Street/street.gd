extends Node2D

@export var _collisionTop: Area2D
@export var _collisionLeft: Area2D
@export var _collisionRight: Area2D
@export var _collisionBottom: Area2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_collisionBottom.area_entered.connect(_on_area_entered)
	_collisionTop.area_entered.connect(_on_area_entered)
	_collisionLeft.area_entered.connect(_on_area_entered)
	_collisionRight.area_entered.connect(_on_area_entered)

	
func _on_area_entered(area: Area2D) -> void:
	if area is Chancla:
		var chancla := area as Chancla
		if not chancla.chancla_exit:
			SignalManager.EmitSignalChanclaExited()
			chancla.chancla_exit = true
