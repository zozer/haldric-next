using Godot;
using System.Collections.Generic;
using Bitron.Ecs;
using Nakama.TinyJson;

public partial class Data : Node
{
    public static Data Instance { get; private set; }

    public Color[] SideColors = new Color[]
    {
        new Color("FF0000"),
        new Color("0000FF"),
        new Color("00FF00"),
        new Color("FFFF00"),
        new Color("00FFFF"),
        new Color("FF00FF"),
        new Color("000000"),
        new Color("FFFFFF"),
    };

    public Dictionary<string, PackedScene> Schedules { get; set; } = new Dictionary<string, PackedScene>();
    public Dictionary<string, PackedScene> Units { get; set; } = new Dictionary<string, PackedScene>();
    public Dictionary<string, FactionData> Factions { get; set; } = new Dictionary<string, FactionData>();
    public Dictionary<string, Dictionary<string, object>> TerrainDicts { get; set; } = new Dictionary<string, Dictionary<string, object>>();
    public Dictionary<string, EcsEntity> Terrains { get; set; } = new Dictionary<string, EcsEntity>();
    public Dictionary<string, MapData> Maps { get; set; } = new Dictionary<string, MapData>();

    public Dictionary<string, Dictionary<string, TerrainGraphic>> Decorations { get; set; } = new Dictionary<string, Dictionary<string, TerrainGraphic>>();
    public Dictionary<string, Dictionary<string, TerrainGraphic>> DirectionalDecorations { get; set; } = new Dictionary<string, Dictionary<string, TerrainGraphic>>();
    public Dictionary<string, TerrainGraphic> WaterGraphics { get; set; } = new Dictionary<string, TerrainGraphic>();
    public Dictionary<string, TerrainGraphic> WallSegments { get; set; } = new Dictionary<string, TerrainGraphic>();
    public Dictionary<string, TerrainGraphic> WallTowers { get; set; } = new Dictionary<string, TerrainGraphic>();
    public Dictionary<string, TerrainGraphic> Cliffs { get; set; } = new Dictionary<string, TerrainGraphic>();
    public Dictionary<string, TerrainGraphic> KeepPlateaus { get; set; } = new Dictionary<string, TerrainGraphic>();
    public Dictionary<string, Texture2D> TerrainTextures { get; set; } = new Dictionary<string, Texture2D>();
    public Dictionary<string, Texture2D> TerrainNormalTextures { get; set; } = new Dictionary<string, Texture2D>();
    public Dictionary<string, Texture2D> TerrainRoughnessTextures { get; set; } = new Dictionary<string, Texture2D>();
    public Dictionary<string, string> DefaultOverlayBaseTerrains = new Dictionary<string, string>();
    
    public Dictionary<string, int> TextureArrayIds { get; set; } = new Dictionary<string, int>();

    public Texture2DArray TextureArray { get; private set; } = new Texture2DArray();
    public Texture2DArray NormalTextureArray { get; private set; } = new Texture2DArray();
    public Texture2DArray RoughnessTextureArray { get; private set; } = new Texture2DArray();

    public override void _Ready()
    {
        Instance = this;
    }

    public void LoadFactions()
    {
        Factions.Clear();

        foreach (var data in Loader.LoadDir("res://data/factions", new List<string>() { "tres" }))
        {
            var faction = (FactionData)data.Data;
            Factions.Add(faction.Name, faction);
        }
    }

    public void LoadUnits()
    {
        Units.Clear();

        foreach (var data in Loader.LoadDir("res://data/units", new List<string>() { "tscn" }))
        {
            Units.Add(data.Id, (PackedScene)data.Data);
        }
    }

    public void LoadSchedules()
    {
        Schedules.Clear();

        foreach (var data in Loader.LoadDir("res://data/schedules", new List<string>() { "tscn" }))
        {
            Schedules.Add(data.Id, (PackedScene)data.Data);
        }
    }

    public void LoadTerrain()
    {
        TerrainDicts.Clear();
        Decorations.Clear();
        WaterGraphics.Clear();
        WallSegments.Clear();
        WallTowers.Clear();
        KeepPlateaus.Clear();
        TerrainTextures.Clear();

        var terrainScript = new TerrainScript();

        terrainScript.Load();

        TerrainDicts = terrainScript.TerrainDicts;

        foreach (var pair in TerrainDicts)
        {
            Terrains.Add(pair.Key, TerrainFactory.CreateFromDict(pair.Value));
        }

        Decorations = terrainScript.Decorations;
        DirectionalDecorations = terrainScript.DirectionalDecorations;
        WaterGraphics = terrainScript.WaterGraphics;
        WallSegments = terrainScript.WallSegments;
        WallTowers = terrainScript.WallTowers;
        Cliffs = terrainScript.Cliffs;
        KeepPlateaus = terrainScript.KeepPlateaus;
        TerrainTextures = terrainScript.TerrainTextures;
        TerrainNormalTextures = terrainScript.TerrainNormalTextures;
        TerrainRoughnessTextures = terrainScript.TerrainRoughnessTextures;
        DefaultOverlayBaseTerrains = terrainScript.DefaultOverlayBaseTerrains;

        CreateTextureArrayIds();

        TextureArray = CreateTextureArray(TextureArray, TerrainTextures);
        NormalTextureArray = CreateTextureArray(NormalTextureArray, TerrainNormalTextures);
        RoughnessTextureArray = CreateTextureArray(RoughnessTextureArray, TerrainRoughnessTextures);
    }

    public void LoadMaps()
    {
        Maps.Clear();

        foreach (var data in Loader.LoadDir("res://data/maps", new List<string>() { "json" }, false))
        {
            var mapData = Loader.LoadJson<MapData>(data.Path);
            Maps.Add(data.Id, mapData);
        }
    }

    public void CreateTextureArrayIds()
    {
        var index = 0;
        foreach (var item in Terrains)
        {
            var terrainCode = item.Key;
            var terrainEntity = item.Value;

            terrainEntity.Add(new TerrainTypeIndex(index));

            TextureArrayIds.Add(terrainCode, index);
            index += 1;
        }
    }

    public Texture2DArray CreateTextureArray(Texture2DArray texArray, Dictionary<string, Texture2D> textureDict)
    {
        var textures = new Godot.Collections.Array();
        textures.Resize(Terrains.Count);

        for (int i = 0; i < textures.Count; i++)
        {
            textures[i] = textureDict["Gg"].GetImage();
        }

        foreach (var item in textureDict)
        {
            var terrainCode = item.Key;
            var terrainTexture = item.Value;

            var index = TextureArrayIds[terrainCode];

            var image = terrainTexture.GetImage();
            textures[index] = image;
        }

        texArray._Images = textures;

        return texArray;
    }

    public EcsEntity CreateTerrain(string terrainType)
    {
        var dict = TerrainDicts[terrainType];
        return TerrainFactory.CreateFromDict(dict);
    }
}
