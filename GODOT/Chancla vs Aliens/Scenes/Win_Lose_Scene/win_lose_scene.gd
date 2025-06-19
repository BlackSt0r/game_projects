extends Control

@export var _labelWinLose: Label
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	SignalManager.Instance.win.connect(LabelText)
	SignalManager.Instance.lose.connect(LabelText)

func _exit_tree() -> void:
	SignalManager.Instance.win.disconnect(LabelText)
	SignalManager.Instance.lose.disconnect(LabelText)

func LabelText(txt: String) -> void:
	_labelWinLose.text = txt
