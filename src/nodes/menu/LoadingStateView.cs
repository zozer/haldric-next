using Godot;

public partial class LoadingStateView : Control
{
    public Label Label = null;

    public override void _Ready()
    {
        Label = GetNode<Label>("ColorRect/CenterContainer/VBoxContainer/Label");
    }
}