using Game.Assets.Scripts.Game.Logic.Aggregations.Building;
using Game.Assets.Scripts.Game.Logic.Aggregations.Fields;
using Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Constructions;
using Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Ghosts;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Services.Building;
using Game.Assets.Scripts.Game.Logic.Services.Field;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Level;

namespace Game.Assets.Scripts.Tests.Setups
{
    public class BuildingSetup : Disposable
    {
        private readonly ServiceManager _services;
        private AssetsMock _assetsMock;

        public BuildingSetup()
        {
            _services = new ServiceManager();
            
            _assetsMock = new AssetsMock();
            var gameAssets = _services.Add(new GameAssetsService(_assetsMock));
            
            // databases
            var ghostDatabase = _services.Add(new SingletonDatabase<GhostEntity>());
            var constructionsDatabase = _services.Add(new Database<ConstructionEntity>());
            var constructionsCardsDatabase = _services.Add(new Database<ConstructionCardEntity>());
            var fieldDatabase = _services.Add(new SingletonDatabase<FieldEntity>());
            
            // repositories
            var ghostRepository = _services.Add(new GhostRepository(ghostDatabase, 
                constructionsDatabase, constructionsCardsDatabase, 
                fieldDatabase));
            var fieldRepository = _services.Add(new FieldRepository(fieldDatabase, constructionsDatabase));
            var constructionsRepository = _services.Add(new FieldConstructionsRepository(constructionsDatabase));
            var constructionBuildingRepository = _services.Add(new BuildingConstructionsRepository(constructionsDatabase));
            var constructionsViewModelRepository = _services.Add(new ConstructionViewModelRepository(constructionsDatabase));
            var ghostViewModelRepository = _services.Add(new GhostViewModelRepository(ghostDatabase));
            
            // services
            _services.Add(new BuildingGhostViewModelService(ghostViewModelRepository, ghostRepository, gameAssets));
            _services.Add(new BuildingConstructionViewModelService(constructionsViewModelRepository, 
                ghostRepository, constructionBuildingRepository));
            _services.Add(new FieldConstructionViewModelService(constructionsViewModelRepository, 
                constructionsRepository, gameAssets));
        }

        protected override void DisposeInner()
        {
            _services.Dispose();
            base.DisposeInner();
        }

        public BuildingSetup FillDefault()
        {
            var field = new FieldEntity();
            _services.Get<SingletonDatabase<FieldEntity>>().Add(field);
            
            var card = new ConstructionCardEntity();
            _services.Get<SingletonDatabase<ConstructionCardEntity>>().Add(card);
            
            return this;
        }
        
        public BuildingSetup Fill(FieldEntity entity)
        {
            var field = _services.Get<SingletonDatabase<FieldEntity>>();
            if (field.Has())
                field.Remove();
            field.Add(entity);
            return this;
        }
        
        public BuildingSetup FillDefaultModel()
        {
            _assetsMock.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionVisualView(x)));
            return this;
        }

        public ConstructionViewModel GetConstructionViewModel(Uid constructionId)
        {
            return _services.Get<ConstructionViewModelRepository>().Get(constructionId);
        }
        
        // public static ConstructionDefinition GetDefaultDefinition()
        // {
        //     var construciton = new ConstructionDefinition()
        //     {
        //         DefId = new DefId("Construction"),
        //         Name = "Name",
        //         Placement = new int[,] {
        //             { 0, 0, 0 },
        //             { 0, 1, 0 },
        //             { 0, 1, 0 },
        //         },
        //         LevelViewPath = "DebugConstruction",
        //         Points = 1,
        //     };
        //     return construciton;
        // }
        //
        // public static ConstructionScheme GetDefaultScheme()
        // {
        //     var placement = new ContructionPlacement(new int[,] {
        //             { 0, 0, 0 },
        //             { 0, 1, 0 },
        //             { 0, 1, 0 },
        //         });
        //     var scheme = new ConstructionScheme(new Uid(),
        //         new DefId("Construction"),
        //         placement,
        //         LocalizationTag.None,
        //         new BuildingPoints(1),
        //         new AdjacencyBonuses(),
        //         "DebugConstruction", "DebugConstruction", new Requirements());
        //     return scheme;
        // }
    }
}