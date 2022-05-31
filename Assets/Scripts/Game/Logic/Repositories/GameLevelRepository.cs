using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class GameLevelRepository : Disposable, IGameLevelPresenterRepository
    {
        public Repository<ConstructionScheme> Schemes { get; private set; } = new();
        public Repository<ConstructionCard> Cards { get; private set; } = new();
        public Repository<Construction> Constructions { get; private set; } = new();
        public Repository<Unit> Units { get; private set; } = new();
        public SingletonRepository<DeckEntity<ConstructionScheme>> ConstructionsDeck { get; private set; } = new();

        IPresenterRepository<ConstructionCard> IGameLevelPresenterRepository.Cards => Cards;
        IPresenterRepository<Construction> IGameLevelPresenterRepository.Constructions => Constructions;
        IPresenterRepository<Unit> IGameLevelPresenterRepository.Units => Units;

        public GameLevelRepository()
        {
        }

        protected override void DisposeInner()
        {
            Cards.Clear();
            Schemes.Clear();
            Constructions.Clear();
            ConstructionsDeck.Clear();
            Units.Clear();
        }
    }
}
