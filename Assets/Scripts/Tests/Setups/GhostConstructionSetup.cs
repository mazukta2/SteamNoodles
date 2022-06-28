using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;

namespace Game.Assets.Scripts.Tests.Setups
{
    public class GhostConstructionSetup : Disposable
    {
        public readonly SingletonDatabase<ConstructionGhost> GhostDatabase;
        public readonly GhostRepository GhostRepository;
        public readonly Database<Construction> ConstructionsDatabase;
        public readonly Database<ConstructionCard> ConstructionsCardsDatabase;
        public readonly SingletonDatabase<Field> FieldDatabase;

        public GhostConstructionSetup()
        {
            GhostDatabase = new SingletonDatabase<ConstructionGhost>();
            ConstructionsDatabase = new Database<Construction>();
            ConstructionsCardsDatabase = new Database<ConstructionCard>();
            FieldDatabase = new SingletonDatabase<Field>();
            GhostRepository = new GhostRepository(GhostDatabase, ConstructionsDatabase, 
                ConstructionsCardsDatabase, FieldDatabase);
        }

        public GhostConstructionSetup FullDefault()
        {
            var field = new Field();
            FieldDatabase.Add(field);
            
            var card = new ConstructionCard();
            ConstructionsCardsDatabase.Add(card);
            
            return this;
        }

        protected override void DisposeInner()
        {
            GhostRepository.Dispose();
            base.DisposeInner();
        }
    }
}