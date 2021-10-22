using Godot;
using Bitron.Ecs;

public class SelectUnitSystem : IEcsSystem
{
    public void Run(EcsWorld world)
    {
        var query = world.Query<HoveredLocation>().End();

        foreach (var hoverEntityId in query)
        {
            var hoverEntity = world.Entity(hoverEntityId);
            var hoveredLocEntity = hoverEntity.Get<HoveredLocation>().Entity;

            if (!hoveredLocEntity.IsAlive())
            {
                return;
            }

            if (Input.IsActionJustPressed("select_unit"))
            {
                if (hoveredLocEntity.Has<HasUnit>() && !hoverEntity.Has<HasLocation>())
                {
                    var unitEntity = hoveredLocEntity.Get<HasUnit>().Entity;
                    var scenario = world.GetResource<Scenario>();

                    if (unitEntity.Get<Team>().Value == scenario.CurrentPlayer)
                    {
                        hoverEntity.Add(new HasLocation(hoveredLocEntity));
                        world.Spawn().Add(new UnitSelectedEvent(unitEntity));
                    }
                }
            }
        }
    }
}