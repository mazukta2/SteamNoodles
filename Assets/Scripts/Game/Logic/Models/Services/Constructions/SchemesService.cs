using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Constructions
{
    public class SchemesService
    {
        private readonly IRepository<ConstructionScheme> _schemes;
        private readonly ISingletonRepository<DeckEntity<ConstructionScheme>> _deck;

        public SchemesService(IRepository<ConstructionScheme> schemes, ISingletonRepository<DeckEntity<ConstructionScheme>> deck)
        {
            _schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));
        }

        public void MakeADeck(IReadOnlyDictionary<ConstructionDefinition, int> deck)
        {
            if (!_deck.Has()) _deck.Add(new DeckEntity<ConstructionScheme>());
            foreach (var item in deck)
                _deck.Get().Add(Find(item.Key), item.Value);
        }

        public void UpdateSchemes(IGameDefinitions definitions)
        {
            var constructionsDefinitions = definitions.GetList<ConstructionDefinition>();
            ConstructionScheme.FillWithDefinitions(constructionsDefinitions, _schemes);
        }

        public ConstructionScheme Add(ConstructionDefinition definition)
        {
            var schemes = ConstructionScheme.FillWithDefinitions(new []{ definition }, _schemes);
            return schemes.First();
        }

        public ConstructionScheme Find(ConstructionDefinition item)
        {
            return _schemes.Get().First(x => x.Definition == item);
        }

        public ConstructionScheme TakeRandom(IGameRandom random)
        {
            return _deck.Get().Take(random);
        }
    }
}
