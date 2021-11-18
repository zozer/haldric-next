using System.Collections.Generic;
using Haldric.Wdk;

public class TerrainScript : TerrainLoader
{
	public override void Load()
	{
		NewBase("Gg", new List<TerrainType>() { TerrainType.Flat });		// grass
		NewBase("Rd", new List<TerrainType>() { TerrainType.Flat });		// dirt
		NewBase("Sd", new List<TerrainType>() { TerrainType.Rough });		// sand desert
		NewBase("Sm", new List<TerrainType>() { TerrainType.Rough }, -0.4f);		// sand mud
		NewBase("Sb", new List<TerrainType>() { TerrainType.Rough }, -0.4f);		// sand beach
		NewBase("Ms", new List<TerrainType>() { TerrainType.Rocky });		// mountains simple

		NewShallowWater("Ws", new List<TerrainType>() { TerrainType.ShallowWaters });		// shallow water
		NewDeepWater("Wo", new List<TerrainType>() { TerrainType.DeepWaters });		// deep water

		NewOverlay("Ff", new List<TerrainType>() { TerrainType.Forested });		// forest

		NewKeep("Kh", new List<TerrainType>() { TerrainType.Fortified });		// keep human
		NewCastle("Ch", new List<TerrainType>() { TerrainType.Fortified });		// castle human

		NewHouses("Vh", new List<TerrainType>() { TerrainType.Settled });		// village human
		NewVillage("VHh", new List<TerrainType>() { TerrainType.Settled, TerrainType.Fortified });		// village hall human

//      Grass  
		AddTerrainTexture("Gg", "assets/graphics/images/grass_basecolor.png");
		AddTerrainNormalTexture("Gg", "assets/graphics/images/grass_normal.png");
		AddTerrainRoughnessTexture("Gg", "assets/graphics/images/grass_roughness.png");

//		Sand desert
		AddTerrainTexture("Sd", "assets/graphics/images/sand_dunes_basecolor.png");
		AddTerrainNormalTexture("Sd", "assets/graphics/images/sand_dunes_normal.png");
		AddTerrainRoughnessTexture("Sd", "assets/graphics/images/sand_dunes_roughness.png");

//		Sand beach
		AddTerrainTexture("Sb", "assets/graphics/images/sand_beach_basecolor.png");
		AddTerrainNormalTexture("Sb", "assets/graphics/images/sand_beach_normal.png");
		AddTerrainRoughnessTexture("Sb", "assets/graphics/images/sand_beach_roughness.png");

//		Sand mud
		AddTerrainTexture("Sm", "assets/graphics/images/mud_basecolor.png");
		AddTerrainNormalTexture("Sm", "assets/graphics/images/mud_normal.png");
		AddTerrainRoughnessTexture("Sm", "assets/graphics/images/mud_roughness.png");

//		Mountains simple
		AddTerrainTexture("Ms", "assets/graphics/images/stone_basecolor.png");
		AddTerrainNormalTexture("Ms", "assets/graphics/images/stone_normal.png");
		AddTerrainRoughnessTexture("Ms", "assets/graphics/images/stone_roughness.png");
		
//		Road dirt
		AddTerrainTexture("Rd", "assets/graphics/images/dirt_basecolor.png");
		AddTerrainNormalTexture("Rd", "assets/graphics/images/dirt_normal.png");
		AddTerrainRoughnessTexture("Rd", "assets/graphics/images/dirt_roughness.png");

//		Castle human
		AddTerrainTexture("Ch", "assets/graphics/images/dirt_castle_basecolor.png");
		AddTerrainNormalTexture("Ch", "assets/graphics/images/dirt_castle_normal.png");
		AddTerrainRoughnessTexture("Ch", "assets/graphics/images/dirt_castle_roughness.png");

//		Keep human
		AddTerrainTexture("Kh", "assets/graphics/images/dirt_castle_basecolor.png");
		AddTerrainNormalTexture("Kh", "assets/graphics/images/dirt_castle_normal.png");
		AddTerrainRoughnessTexture("Kh", "assets/graphics/images/dirt_castle_roughness.png");

//		Water shallow
		AddTerrainTexture("Ws", "assets/graphics/images/mud_basecolor.png");
		AddTerrainNormalTexture("Ws", "assets/graphics/images/mud_normal.png");
		AddTerrainRoughnessTexture("Ws", "assets/graphics/images/mud_roughness.png");
		
//		Water deep
		AddTerrainTexture("Wo", "assets/graphics/images/mud_basecolor.png");
		AddTerrainNormalTexture("Wo", "assets/graphics/images/mud_normal.png");
		AddTerrainRoughnessTexture("Wo", "assets/graphics/images/mud_roughness.png");
		
		// AddKeepPlateauGraphic("Kh", "assets/graphics/models/keep_plateau.tres", new Godot.Vector3(0f, 1.5f, 0f));
		AddWallSegmentGraphic("Kh", "assets/graphics/models/keep_wall.tres");
		AddWallTowerGraphic("Kh", "assets/graphics/models/keep_tower.tres");

		// AddKeepPlateauGraphic("Ch", "assets/graphics/models/castle_plateau.tres", new Godot.Vector3(0f, 0.05f, 0f));
		AddWallSegmentGraphic("Ch", "assets/graphics/models/castle_wall.tres");
		AddWallTowerGraphic("Ch", "assets/graphics/models/castle_tower.tres");
		
		AddCliffGraphic("Gg", "assets/graphics/models/cliff.tres");

		AddDecorationGraphic("Ff", "assets/graphics/models/forest_pine_center_01.tres", "center");
		AddDecorationGraphic("Ff", "assets/graphics/models/forest_pine_center_02.tres", "center");

		AddDirectionalDecorationGraphic("Ff", "assets/graphics/models/forest_pine_outer_01.tres", "outer");
		AddDirectionalDecorationGraphic("Ff", "assets/graphics/models/forest_pine_outer_02.tres", "outer");
		AddDirectionalDecorationGraphic("Ff", "assets/graphics/models/forest_pine_outer_03.tres", "outer");

		AddDecorationGraphic("Vh", "assets/graphics/models/Village_human_center_01.tres", "center");
		AddDecorationGraphic("Vh", "assets/graphics/models/Village_human_center_02.tres", "center");
		AddDecorationGraphic("Vh", "assets/graphics/models/Village_human_center_03.tres", "center");

		AddDirectionalDecorationGraphic("Vh", "assets/graphics/models/Village_human_outer_01.tres", "outer");
		AddDirectionalDecorationGraphic("Vh", "assets/graphics/models/Village_human_outer_02.tres", "outer");
		AddDirectionalDecorationGraphic("Vh", "assets/graphics/models/Village_human_outer_03.tres", "outer");
		AddDirectionalDecorationGraphic("Vh", "assets/graphics/models/Village_human_outer_04.tres", "outer");

		AddDecorationGraphic("VHh", "assets/graphics/models/Village_human_hall_base_01.tres");
		AddDecorationGraphic("VHh", "assets/graphics/models/Village_human_hall_center_01.tres");
		AddDirectionalDecorationGraphic("VHh", "assets/graphics/models/Village_human_hall_outer_01.tres", "outer");
		AddDirectionalDecorationGraphic("VHh", "assets/graphics/models/Village_human_hall_outer_02.tres", "outer");
		AddDirectionalDecorationGraphic("VHh", "assets/graphics/models/Village_human_hall_outer_03.tres", "outer");

		AddWaterGraphic("Ws", "assets/graphics/models/water.tres");
		AddWaterGraphic("Wo", "assets/graphics/models/water.tres");
	}
}
