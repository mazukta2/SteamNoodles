using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class Recipe
    {
        private GameLevel _level;
        private IRecipePrototype _proto;
        private bool _isOpen;

        public Recipe(GameLevel level, IRecipePrototype proto)
        {
            _level = level;
            _proto = proto;
            _isOpen = true;
        }

        public void Close()
        {
            if (!IsCanBeClosed())
                throw new Exception("Can be closed");

            _isOpen = false;
        }

        public bool IsOpen()
        {
            return _isOpen;
        }

        public bool IsCanBeClosed()
        {
            return _level.Placement.Constructions.Any(x => x.IsProvide(_proto.Ingredient));
        }
    }
}
