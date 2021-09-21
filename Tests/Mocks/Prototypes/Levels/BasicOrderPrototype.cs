using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Prototypes.Levels;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Mocks.Prototypes.Levels
{
    public class BasicOrderPrototype : IOrderPrototype
    {
        public List<IIngredientPrototype> Ingredient { get; set; } = new List<IIngredientPrototype>();
        public IIngredientPrototype[] RequiredIngredients => Ingredient.ToArray();
    }
}
