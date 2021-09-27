using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Assets.Scripts.Game.Logic.Models.Orders
{
    public class Recipe
    {
        private IRecipePrototype _proto;

        public Recipe(IRecipePrototype proto)
        {
            _proto = proto;
        }
    }
}
