using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units
{
    public class UnitsTypesService : IService
    {
        private IRepository<UnitType> _types;
        private DeckService<UnitType> _deck;

        public UnitsTypesService(IRepository<UnitType> types, DeckService<UnitType> deck, StageLevel level)
        {
            _types = types ?? throw new ArgumentNullException(nameof(types));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));

            if (level.CrowdUnitsAmount != 0 && level.CrowdUnits.Count == 0)
                throw new Exception("Not enought units");

            foreach (var item in level.CrowdUnits)
                _deck.Add(item.Key, item.Value);
        }

        public UnitsTypesService(IRepository<UnitType> types, DeckService<UnitType> deck)
        {
            _types = types ?? throw new ArgumentNullException(nameof(types));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));

            if (types.Count == 0)
                throw new Exception("Not enought units");

            if (_deck.IsEmpty())
                throw new Exception("Not enought units");
        }

        public UnitType TakeRandom()
        {
            return _deck.Take();
        }
    }
}
