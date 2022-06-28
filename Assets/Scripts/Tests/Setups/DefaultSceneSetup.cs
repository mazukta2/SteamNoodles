using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Constructions;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Screens;

namespace Game.Assets.Scripts.Tests.Setups
{
    public class DefaultSceneSetup
    {
        // private AssetsMock _assets;
        // private DefinitionsMock _definitions;
        //
        // public DefaultSceneSetup(AssetsMock assets, DefinitionsMock definitions)
        // {
        //     _assets = assets;
        //     _definitions = definitions;
        // }
        //
        // public void Create()
        // {
        //     _assets.AddPrefab("Screens/MainScreen", new MainScreenPrefab());
        //     _assets.AddPrefab("Screens/BuildScreen", new BuildScreenPrefab());
        //     _assets.AddPrefab("Screens/DayEndedScreen", new DayEndedScreenPrefab());
        //     _assets.AddPrefab("Screens/GameMenuScreen", new GameMenuScreenPrefab());
        //
        //     _definitions.Add(nameof(ConstructionsSettingsDefinition), new ConstructionsSettingsDefinition()
        //     {
        //         CellSize = 0.5f,
        //         LevelUpPower = 2,
        //         LevelUpOffset =2,
        //     });
        //     _assets.AddPrefab("DebugConstruction", new BasicConstructionModelPrefab());
        //     _definitions.Add(nameof(UnitsSettingsDefinition), UnitDefinitionSetup.GetDefaultUnitsDefinitions());
        //
        //     var construciton = ConstructionSetups.GetDefaultDefinition();
        //     _definitions.Add("Construction1", construciton);
        //
        //     var customer = new CustomerDefinition()
        //     {
        //     };
        //     _definitions.Add("Customer1", customer);
        //
        //     var level = LevelDefinitionSetups.GetDefault(customer, construciton);
        //     _definitions.Add(level.DefId.Path, level);
        //
        //     _definitions.Add(nameof(MainDefinition), new MainDefinition() { StartLevel = level }) ;
        // }

    }
}