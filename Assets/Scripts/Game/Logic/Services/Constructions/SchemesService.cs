using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Common;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions
{
    public class SchemesService : IService
    {
        private readonly IDatabase<ConstructionSchemeEntity> _schemes;
        private readonly DeckService<ConstructionSchemeEntity> _deck;

        public SchemesService(IDatabase<ConstructionSchemeEntity> schemes, DeckService<ConstructionSchemeEntity> deck)
        {
            _schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));
        }

        public SchemesService(IDatabase<ConstructionSchemeEntity> schemes, DeckService<ConstructionSchemeEntity> deck,
            IReadOnlyDictionary<ConstructionSchemeEntity, int> availableConstructions)
        {
            _schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));

            foreach (var item in availableConstructions)
                _deck.Add(item.Key, item.Value);
        }

        public ConstructionSchemeEntity TakeRandom()
        {
            return _deck.Take();
        }

        private ConstructionSchemeEntity Find(ConstructionDefinition item)
        {
            return _schemes.Get().First(x => x.IsConnectedToDefinition(item));
        }

    }
}
