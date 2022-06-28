using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Common;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions
{
    public class SchemesService : IService
    {
        private readonly IDatabase<ConstructionScheme> _schemes;
        private readonly DeckService<ConstructionScheme> _deck;

        public SchemesService(IDatabase<ConstructionScheme> schemes, DeckService<ConstructionScheme> deck)
        {
            _schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));
        }

        public SchemesService(IDatabase<ConstructionScheme> schemes, DeckService<ConstructionScheme> deck,
            IReadOnlyDictionary<ConstructionScheme, int> availableConstructions)
        {
            _schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));

            foreach (var item in availableConstructions)
                _deck.Add(item.Key, item.Value);
        }

        public ConstructionScheme TakeRandom()
        {
            return _deck.Take();
        }

        private ConstructionScheme Find(ConstructionDefinition item)
        {
            return _schemes.Get().First(x => x.IsConnectedToDefinition(item));
        }

    }
}
