using Game.Assets.Scripts.Game.Logic.Models.Entities.Common;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
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
        private readonly DeckService<ConstructionScheme> _deck;

        public SchemesService(IRepository<ConstructionScheme> schemes, DeckService<ConstructionScheme> deck)
        {
            _schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));
        }

        public SchemesService(IGameDefinitions definitions, 
            IReadOnlyDictionary<ConstructionDefinition, int> availableConstructions,
            IRepository<ConstructionScheme> schemes, DeckService<ConstructionScheme> deck)
        {
            _schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));

            var constructionsDefinitions = definitions.GetList<ConstructionDefinition>();
            ConstructionScheme.FillWithDefinitions(constructionsDefinitions, _schemes);

            foreach (var item in availableConstructions)
                _deck.Add(Find(item.Key), item.Value);
        }

        public ConstructionScheme Add(ConstructionDefinition definition)
        {
            var schemes = ConstructionScheme.FillWithDefinitions(new []{ definition }, _schemes);
            return schemes.First();
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
