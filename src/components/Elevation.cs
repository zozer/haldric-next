using Bitron.Ecs;

public struct Elevation : IEcsAutoReset<Elevation>
{
    public int Value;
    public int Offset;

    public float Height { get { return Value * Metrics.ElevationStep; }}
    public float HeightWithOffset { get { return (Value + Offset) * Metrics.ElevationStep; }}
    
    public Elevation(int value, int offset = 0)
    {
        Value = value;
        Offset = offset;
    }

    public void AutoReset(ref Elevation c)
    {
        c.Value = 0;
    }
}