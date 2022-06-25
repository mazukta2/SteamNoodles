using System;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Common;

namespace Game.Assets.Scripts.Game.Logic.Services.Units
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
