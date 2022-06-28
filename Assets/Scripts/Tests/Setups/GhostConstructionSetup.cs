using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories.Fields;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Level;

namespace Game.Assets.Scripts.Tests.Setups
{
    public class GhostConstructionSetup : Disposable
    {
        public SingletonDatabase<GhostEntity> GhostDatabase { get;private set; }
        public GhostRepository GhostRepository { get; private set; }
        public Database<ConstructionEntity> ConstructionsDatabase{ get; private set; }
        public Database<ConstructionCardEntity> ConstructionsCardsDatabase{ get; private set; }
        public SingletonDatabase<FieldEntity> FieldDatabase{ get; private set; }
        public FieldRepository FieldRepository{ get; private set; }
        public ConstructionsRepository ConstructionsRepository{ get; private set; }
        public AssetsMock AssetsMock { get; private set; }
        public GameAssetsService GameAssets { get; private set; }
        

        public GhostConstructionSetup()
        {
            AssetsMock = new AssetsMock();
            GameAssets = new GameAssetsService(AssetsMock);
            
            GhostDatabase = new SingletonDatabase<GhostEntity>();
            ConstructionsDatabase = new Database<ConstructionEntity>();
            ConstructionsCardsDatabase = new Database<ConstructionCardEntity>();
            FieldDatabase = new SingletonDatabase<FieldEntity>();
            
            GhostRepository = new GhostRepository(GhostDatabase, ConstructionsDatabase, ConstructionsCardsDatabase, 
                FieldDatabase);
            FieldRepository = new FieldRepository(FieldDatabase, ConstructionsDatabase);
            ConstructionsRepository = new ConstructionsRepository(ConstructionsDatabase, GameAssets);
        }

        protected override void DisposeInner()
        {
            GhostRepository.Dispose();
            FieldRepository.Dispose();
            ConstructionsRepository.Dispose();
            base.DisposeInner();
        }

        public GhostConstructionSetup FillDefault()
        {
            var field = new FieldEntity();
            FieldDatabase.Add(field);
            
            var card = new ConstructionCardEntity();
            ConstructionsCardsDatabase.Add(card);
            
            return this;
        }
        
        public GhostConstructionSetup Fill(FieldEntity entity)
        {
            if (FieldDatabase.Has())
                FieldDatabase.Remove();
            FieldDatabase.Add(entity);
            return this;
        }
        
        public GhostConstructionSetup FillDefaultModel()
        {
            AssetsMock.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            return this;
        }
    }
}