using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Common;
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
    public class StageLevelRepository : Disposable, IStageLevelPresenterRepositories
    {
        public Repository<ConstructionScheme> Schemes { get; private set; } = new();
        public Repository<ConstructionCard> Cards { get; private set; } = new();
        public Repository<Construction> Constructions { get; private set; } = new();
        public Repository<Unit> Units { get; private set; } = new();
        public SingletonRepository<Deck<ConstructionScheme>> ConstructionsDeck { get; private set; } = new();

        IPresenterRepository<ConstructionCard> IStageLevelPresenterRepositories.Cards => Cards;
        IPresenterRepository<Construction> IStageLevelPresenterRepositories.Constructions => Constructions;
        IPresenterRepository<Unit> IStageLevelPresenterRepositories.Units => Units;

        public StageLevelRepository()
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
