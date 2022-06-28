using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using System;
using System.Collections.Generic;
using System.Text;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories.Fields;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Level;

namespace Game.Assets.Scripts.Tests.Setups
{
    public class ConstructionSetup : Disposable
    {
        public Database<ConstructionEntity> ConstructionsDatabase { get; private set; }
        public SingletonDatabase<FieldEntity> FieldDatabase { get; private set; }
        public ConstructionsRepository ConstructionsRepository { get; private set; }
        public FieldRepository FieldRepository { get; private set; }
        public AssetsMock AssetsMock { get; private set; }
        public GameAssetsService GameAssets { get; private set; }
        
        public ConstructionSetup()
        {
            AssetsMock = new AssetsMock();
            GameAssets = new GameAssetsService(AssetsMock);
            
            ConstructionsDatabase = new Database<ConstructionEntity>();
            FieldDatabase = new SingletonDatabase<FieldEntity>();
            ConstructionsRepository = new ConstructionsRepository(ConstructionsDatabase, GameAssets);
            FieldRepository = new FieldRepository(FieldDatabase, ConstructionsDatabase);
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            ConstructionsRepository.Dispose();
            FieldRepository.Dispose();
        }

        public ConstructionSetup FillDefault()
        {
            Fill(new FieldEntity());
            FillDefaultModel();
            
            return this;
        }
        
        public ConstructionSetup Fill(FieldEntity entity)
        {
            FieldDatabase.Add(entity);
            return this;
        }

        public ConstructionSetup FillDefaultModel()
        {
            AssetsMock.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            return this;
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
