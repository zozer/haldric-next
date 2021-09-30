using System.Collections.Generic;
using Bitron.Ecs;

public class TerrainFactory
{
    private static TerrainBuilder _builder = new TerrainBuilder();

    public static EcsEntity CreateFromDict(Dictionary<string, object> dict)
    {
        if (dict.ContainsKey("BaseTerrain"))
        {
            _builder.CreateBase();
        }

        if (dict.ContainsKey("OverlayTerrain"))
        {
            _builder.CreateOverlay();
        }

        if (dict.ContainsKey("TerrainCode"))
        {
            _builder.WithCode((string)dict["TerrainCode"]);
        }

        if (dict.ContainsKey("TerrainTypes"))
        {
            _builder.WithTypes((List<TerrainType>)dict["TerrainTypes"]);
        }

        if (dict.ContainsKey("HasWater"))
        {
            _builder.WithHasWater();
        }

        if (dict.ContainsKey("RecruitFrom"))
        {
            _builder.WithRecruitFrom();
        }

        if (dict.ContainsKey("RecruitTo"))
        {
            _builder.WithRecruitTo();
        }

        return _builder.Build();
    }
}