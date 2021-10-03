using Godot;
using Bitron.Ecs;

public struct Chunk { }
public struct Map { }

public struct ChunkSize : IEcsAutoReset<ChunkSize>
{
    public int X;
    public int Z;

    public ChunkSize(int x, int z)
    {
        X = x;
        Z = z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(X, 0f, Z);
    }

    public void AutoReset(ref ChunkSize c)
    {
        c.X = 0;
        c.Z = 0;
    }
}