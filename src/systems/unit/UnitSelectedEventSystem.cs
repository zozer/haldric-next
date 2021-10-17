using Godot;
using Bitron.Ecs;

public struct UnitSelectedEvent
{
    public EcsEntity Unit;

    public UnitSelectedEvent(EcsEntity unit)
    {
        Unit = unit;
    }
}

public class UnitSelectedEventSystem : IEcsSystem
{
    public void Run(EcsWorld world)
    {
        var query = world.Query<UnitSelectedEvent>().End();

        foreach (var eventEntityId in query)
        {
            var unitEntity = world.Entity(eventEntityId).Get<UnitSelectedEvent>().Unit;

            var id = unitEntity.Get<Id>().Value;
            var hp = unitEntity.Get<Attribute<Health>>();
            var xp = unitEntity.Get<Attribute<Experience>>();
            var mp = unitEntity.Get<Attribute<Moves>>();

            var s = string.Format("Id: {0}\nHP: {1}\nXP: {2}\nMP: {3}", id, hp.ToString(), xp.ToString(), mp.ToString());

            if (unitEntity.Has<Attacks>())
            {
                s += "\nAttacks:";
                foreach(EcsEntity attackEntity in unitEntity.Get<Attacks>().GetList())
                {
                    ref var attackId = ref attackEntity.Get<Id>();
                    ref var damage = ref attackEntity.Get<Damage>();
                    ref var strikes = ref attackEntity.Get<Strikes>();
                    ref var range = ref attackEntity.Get<Range>();
                    s += string.Format("\n{0} {1}x{2} ({3}) ({4})", attackId.Value, damage.Value, strikes.Value, damage.Type.ToString(), range.Value.ToString());
                }
            }

            var hudView = world.GetResource<HudView>();
            hudView.UnitLabel.Text = s;

            var coords = unitEntity.Get<Coords>();
            
            world.Spawn().Add(new HighlightLocationEvent(coords, mp.Value));
        }
    }
}