using Leopotam.Ecs;

public partial class EditorState : GameState
{
    EcsEntity _editorEntity;

    public EditorState(EcsWorld world) : base(world)
    {
        AddInputSystem(new UpdateMapCursorSystem(this));
        AddInputSystem(new EditorEditSystem(this));
        AddInputSystem(new SelectLocationSystem(this));
        AddInputSystem(new UpdateTerrainInfoSystem());
        AddInputSystem(new LocationHighlightSystem());

        AddUpdateSystem(new UpdateStatsInfoSystem(this));

        // AddEventSystem<CreateUnitEvent>(new CreateUnitEventSystem(this));
        AddEventSystem<UpdateMapEvent>(new UpdateMapEventSystem());
        AddEventSystem<UpdateTerrainMeshEvent>(new UpdateTerrainMeshEventSystem());
        AddEventSystem<UpdateTerrainFeaturePopulatorEvent>(new UpdateTerrainFeaturePopulatorEventSystem());
        AddEventSystem<SaveMapEvent>(new SaveMapEventSystem());
        AddEventSystem<LoadMapEvent>(new LoadMapEventSystem());
        AddEventSystem<DestroyMapEvent>(new DestroyMapEventSystem());
        AddEventSystem<CreateMapEvent>(new CreateMapEventSystem(this));
    }

    public override void Enter(GameStateController gameStates)
    {
        _editorEntity = _world.NewEntity();

        var editorView = Scenes.Instance.EditorView.Instantiate<EditorView>();
        AddChild(editorView);

        _editorEntity.Replace(new NodeHandle<EditorView>(editorView));

        _world.NewEntity().Replace(new CreateMapEvent(40, 40));
    }

    public override void Exit(GameStateController gameStates)
    {
        _world.NewEntity().Replace(new DestroyMapEvent());
        _editorEntity.Destroy();
    }

    public override void Input(GameStateController gameStates, Godot.InputEvent e)
    {
        if (e.IsActionPressed("ui_cancel"))
        {
            gameStates.ChangeState(new MainMenuState(_world));
        }
    }
}