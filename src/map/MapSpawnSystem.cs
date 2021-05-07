using Godot;
using Leopotam.Ecs;

struct Forest { }
struct Castle { }

public class MapSpawnSystem : IEcsInitSystem
{
    EcsWorld _world;
    Node _parent;

    public MapSpawnSystem(Node parent)
    {
        _parent = parent;
    }

    public void Init()
    {
        GD.Print("New Map Created!");

        var mapEntity = _world.NewEntity();

        mapEntity.Replace(new Grid(40, 40));

        var terrainMesh = new TerrainMesh();
        var terrainCollider = new TerrainCollider();
        var terrainFeaturePopulator = new TerrainFeaturePopulator();
        
        _parent.AddChild(terrainMesh);
        _parent.AddChild(terrainCollider);
        _parent.AddChild(terrainFeaturePopulator);
        
        mapEntity.Replace(new NodeHandle<TerrainCollider>(terrainCollider));
        mapEntity.Replace(new NodeHandle<TerrainFeaturePopulator>(terrainFeaturePopulator));
        mapEntity.Replace(new NodeHandle<TerrainMesh>(terrainMesh));

        ref var locations = ref mapEntity.Get<Locations>();
        ref var grid = ref mapEntity.Get<Grid>();

        InitializeLocations(grid, locations);
        InitializeNeighbors(locations);

        SendUpdateMapEvent();
    }

    private void InitializeLocations(Grid grid, Locations locations)
    {
        for (int z = 0; z < grid.Height; z++)
        {
            for (int x = 0; x < grid.Height; x++)
            {
                var locEntity = _world.NewEntity();

                locEntity.Replace(Coords.FromOffset(x, z));

                // Mountains
                if (GD.Randf() < 0.1)
                {
                    locEntity.Replace(new Elevation(5, GD.Randf() * 1.5f + 1.5f));
                    locEntity.Replace(new PlateauArea(0.75f));
                }
                // Normal
                else
                {
                    locEntity.Replace(new Elevation((int)GD.Randi() % 3, 2.0f));
                    locEntity.Replace(new PlateauArea(0.75f));

                    // Forest
                    if (GD.Randf() < 0.2)
                    {
                        locEntity.Get<Forest>();
                    }
                    else if (GD.Randf() < 0.3)
                    {
                        locEntity.Get<Castle>();
                    }
                }

                ref var coords = ref locEntity.Get<Coords>();

                locations.Set(coords.Cube, locEntity);
            }
        }
    }

    private void InitializeNeighbors(Locations locations)
    {
        foreach (var entity in locations.Values)
        {
            ref var coords = ref entity.Get<Coords>();
            ref var neighbors = ref entity.Get<Neighbors>();

            for (Direction direction = Direction.NE; direction <= Direction.SE; direction++)
            {
                Vector3 nCell = Hex.GetNeighbor(coords.Cube, direction);

                if (!locations.Has(nCell))
                {
                    continue;
                }

                var nEntity = locations.Get(nCell);
                neighbors.Set(direction, nEntity);
            }
        }
    }

    private void SendUpdateMapEvent()
    {
        var updateMapEventEntity = _world.NewEntity();
        updateMapEventEntity.Get<UpdateMapEvent>();
    }
}