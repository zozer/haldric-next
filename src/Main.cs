using Godot;
using Bitron.Ecs;

public partial class Main : Node3D
{
    public static Main Instance { get; private set; }

    private GameStateController _gameController = new GameStateController();

    public EcsWorld World { get; private set; } = new EcsWorld();

    public override void _Ready()
    {
        Instance = this;

        Data.Instance.Scan();
        
        _gameController.Name = "GameStateController";
        AddChild(_gameController);

        World.AddResource(_gameController);
        World.AddResource(GetTree());

        _gameController.PushState(new ApplicationState(World));
    }

    public override void _ExitTree()
    {
        World.Destroy();
    }
}
