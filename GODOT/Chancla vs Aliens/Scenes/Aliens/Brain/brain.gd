extends Alien

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	area_entered.connect(HitEnemy)
	SignalManager.Instance.game_pause.connect(AlienPause)
	SignalManager.Instance.win.connect(Pause)
	SignalManager.Instance.lose.connect(DesactivateCollision)


func _exit_tree() -> void:
	SignalManager.Instance.game_pause.disconnect(AlienPause)
	SignalManager.Instance.win.disconnect(Pause)
	SignalManager.Instance.lose.disconnect(DesactivateCollision)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	super._process(delta)

func HitEnemy(body: Area2D) -> void:
	if (not _alreadyHit) and body is Chancla:
		SignalManager.EmitSignalHitEnemy()
		_alreadyHit = true
		Chau()
