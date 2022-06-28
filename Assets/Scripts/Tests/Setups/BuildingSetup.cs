using Game.Assets.Scripts.Game.Logic.Aggregations.Building;
using Game.Assets.Scripts.Game.Logic.Aggregations.Fields;
using Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Constructions;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Level;

namespace Game.Assets.Scripts.Tests.Setups
{
    public class BuildingSetup : Disposable
    {
        private readonly ServiceManager _services;
        public SingletonDatabase<GhostEntity> GhostDatabase { get;private set; }
        public GhostRepository GhostRepository { get; private set; }
        public Database<ConstructionEntity> ConstructionsDatabase{ get; private set; }
        public Database<ConstructionCardEntity> ConstructionsCardsDatabase{ get; private set; }
        public SingletonDatabase<FieldEntity> FieldDatabase{ get; private set; }
        public FieldRepository FieldRepository{ get; private set; }
        public ConstructionsRepository ConstructionsRepository{ get; private set; }
        public AssetsMock AssetsMock { get; private set; }
        public GameAssetsService GameAssets { get; private set; }
        

        public BuildingSetup()
        {
            _services = new ServiceManager();
            
            AssetsMock = new AssetsMock();
            GameAssets = _services.Add(new GameAssetsService(AssetsMock));
            
            GhostDatabase = _services.Add(new SingletonDatabase<GhostEntity>());
            ConstructionsDatabase = _services.Add(new Database<ConstructionEntity>());
            ConstructionsCardsDatabase = _services.Add(new Database<ConstructionCardEntity>());
            FieldDatabase = _services.Add(new SingletonDatabase<FieldEntity>());
            
            GhostRepository = _services.Add(new GhostRepository(GhostDatabase, ConstructionsDatabase, ConstructionsCardsDatabase, 
                FieldDatabase));
            FieldRepository = _services.Add(new FieldRepository(FieldDatabase, ConstructionsDatabase));
            ConstructionsRepository = _services.Add(new ConstructionsRepository(ConstructionsDatabase));
            
            _services.Add(new ConstructionViewModelRepository(ConstructionsDatabase));
        }

        protected override void DisposeInner()
        {
            _services.Dispose();
            base.DisposeInner();
        }

        public BuildingSetup FillDefault()
        {
            var field = new FieldEntity();
            FieldDatabase.Add(field);
            
            var card = new ConstructionCardEntity();
            ConstructionsCardsDatabase.Add(card);
            
            return this;
        }
        
        public BuildingSetup Fill(FieldEntity entity)
        {
            if (FieldDatabase.Has())
                FieldDatabase.Remove();
            FieldDatabase.Add(entity);
            return this;
        }
        
        public BuildingSetup FillDefaultModel()
        {
            AssetsMock.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionVisualView(x)));
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