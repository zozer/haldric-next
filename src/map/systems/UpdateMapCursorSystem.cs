using Godot;
using Godot.Collections;
using Bitron.Ecs;

public class UpdateMapCursorSystem : IEcsSystem
{
    private Node3D _parent;

    private Vector3 previousCell = Vector3.Zero;

    public UpdateMapCursorSystem(Node3D parent)
    {
        _parent = parent;
    }

    public void Run(EcsWorld world)
    {
        var cursorQuery = world.Query<HoveredLocation>().End();
        var mapQuery = world.Query<Locations>().Inc<Map>().End();

        foreach (var mapEntityId in mapQuery)
        {
            ref var locations = ref mapQuery.Get<Locations>(mapEntityId);

            var result = ShootRay();

            if (result.Contains("position"))
            {
                var position = (Vector3)result["position"];
                var coords = Coords.FromWorld(position);

                if (previousCell != coords.Axial)
                {
                    foreach (var cursorEntityId in cursorQuery)
                    {
                        var locEntity = locations.Get(coords.Cube);
                        cursorQuery.Get<HoveredLocation>(cursorEntityId).Entity = locEntity;
                    }

                    previousCell = coords.Axial;
                }
            }
        }
    }

    private Dictionary ShootRay()
    {
        var spaceState = _parent.GetWorld3d().DirectSpaceState;
        var viewport = _parent.GetViewport();

        var camera = viewport.GetCamera3d();
        var mousePosition = viewport.GetMousePosition();

        var from = camera.ProjectRayOrigin(mousePosition);
        var to = from + camera.ProjectRayNormal(mousePosition) * 1000f;

        return spaceState.IntersectRay(from, to);
    }
}